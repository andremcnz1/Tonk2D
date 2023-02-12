using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum CharacterType
{
    Player,
    AI
};

public class TankController : MonoBehaviour
{
    private CharacterType characterType;

    [System.Serializable]
    public class PlayerInfo
    {
        [SerializeField] public float RotationSpeed = 10.0f;  // Conditional show/hide in inspector
        [SerializeField] public float Acceleration = 1.5f;    // Conditional show/hide in inspector
        [SerializeField] public float Deceleration = 8.0f;    // Conditional show/hide in inspector
        [SerializeField] public float MaxSpeed = 1.0f;        // Conditional show/hide in inspector
        [SerializeField] public float TurretSpeed = 50.0f;    // Conditional show/hide in inspector
        [SerializeField] public float FiringRateDelay = 1.5f; // Conditional show/hide in inspector

        [NonSerialized] public float FiringRateTime = 0.0f;
        [NonSerialized] public Vector3 CurrentSpeed;
        [NonSerialized] public float Rotation;
        [NonSerialized] public Vector2 movement;
        [NonSerialized] public Vector2 MousePos;
        
    }

    [System.Serializable]
    public class AIInfo
    {
        [SerializeField] public GameObject Player;     // Conditional show/hide in inspector 
        [SerializeField] public float speed;           // Conditional show/hide in inspector 
        [SerializeField] public float distanceBetween; // Conditional show/hide in inspector 

        [NonSerialized] public float distance;
    }

    [System.Serializable]
    public class UniversalInfo
    {
        [SerializeField] public Transform Turret;
        [SerializeField] public GameObject R35Bullet;
        [SerializeField] public Transform FiringPosition;
        [SerializeField] public float BulletSpeed = 4.0f;
    }

    [SerializeField] private AIInfo aiInfo = new AIInfo();
    [SerializeField] private PlayerInfo playerInfo = new PlayerInfo();
    [SerializeField] private UniversalInfo universalInfo = new UniversalInfo();

    private void Awake()
    {
        if(gameObject.tag == "AI")
        {
            characterType = CharacterType.AI;
        } else if(gameObject.tag == "LocalPlayer")
        {
            characterType = CharacterType.Player;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInfo.CurrentSpeed = Vector3.zero;
    }

    void Update()
    {
        if (characterType == CharacterType.Player)
        {
            playerInfo.MousePos = Input.mousePosition;
            playerInfo.movement.x = Input.GetAxisRaw("Horizontal");
            playerInfo.movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Fire1") && playerInfo.FiringRateTime >= playerInfo.FiringRateDelay)
            {
                Shoot(universalInfo.R35Bullet, universalInfo.FiringPosition, universalInfo.BulletSpeed);
                playerInfo.FiringRateTime = 0f;
            }

            playerInfo.FiringRateTime += Time.deltaTime;
        }
        else if(characterType == CharacterType.AI)
        {
            aiInfo.distance = Vector2.Distance(transform.position, aiInfo.Player.transform.position);
            Vector2 direction = aiInfo.Player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (aiInfo.distance < aiInfo.distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, aiInfo.Player.transform.position, aiInfo.speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * (angle - 90.0f));
            }
        }
    }

    void FixedUpdate()
    {
        if (characterType == CharacterType.Player)
        {
            playerInfo.Rotation = playerInfo.movement.x * -playerInfo.RotationSpeed * Time.deltaTime;

            if (playerInfo.movement.y != 0)
            {
                playerInfo.CurrentSpeed += new Vector3(0, playerInfo.movement.y * playerInfo.Acceleration * Time.fixedDeltaTime, 0);
                if (playerInfo.CurrentSpeed.y > playerInfo.MaxSpeed)
                    playerInfo.CurrentSpeed.y = playerInfo.MaxSpeed;
            }
            else
            {
                playerInfo.CurrentSpeed.y = Mathf.Lerp(playerInfo.CurrentSpeed.y, 0, playerInfo.Deceleration * Time.deltaTime);
            }
        }
    }

    private void LateUpdate()
    {
        if (characterType == CharacterType.Player)
        {
            UpdateTurretRotation();
            transform.Translate(playerInfo.CurrentSpeed * playerInfo.Acceleration * Time.deltaTime);
            transform.Rotate(0f, 0f, playerInfo.Rotation);
        }
    }

    private void UpdateTurretRotation()
    {
        if (characterType == CharacterType.Player)
        {
            Vector2 ObjectPos = Camera.main.WorldToScreenPoint(universalInfo.Turret.position);
            playerInfo.MousePos = playerInfo.MousePos - ObjectPos;
            float angle = Mathf.Atan2(playerInfo.MousePos.y, playerInfo.MousePos.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            universalInfo.Turret.rotation = Quaternion.RotateTowards(universalInfo.Turret.rotation, target, Time.deltaTime * playerInfo.TurretSpeed);
        }
    }

    public void Shoot(GameObject bulletPrefab, Transform firingPosition, float bulletSpeed)
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPosition.position, firingPosition.rotation);
        bullet.GetComponent<Bullet>().SetParent(gameObject);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firingPosition.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
