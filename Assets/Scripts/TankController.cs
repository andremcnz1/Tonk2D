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

[CustomEditor(typeof(Editor))]
public class TankController : MonoBehaviour
{
    private CharacterType characterType;

    /* Player variables */

    [SerializeField] private float RotationSpeed = 10.0f;
    [SerializeField] private float Acceleration = 1.5f;
    [SerializeField] private float Deceleration = 8.0f;
    [SerializeField] private float MaxSpeed = 1.0f;
    [SerializeField] private float TurretSpeed = 50.0f;
    [SerializeField] private float FiringRateDelay = 1.5f;


    private float FiringRateTime = 0.0f;

    private Vector3 CurrentSpeed;
    private float Rotation;
    private Vector2 movement;
    private Vector2 MousePos;

    /* AI variables */

    [SerializeField] private GameObject Player;
    [SerializeField] private float speed;
    [SerializeField] private float distanceBetween;

    private float distance;

    /* Universial variables */

    [SerializeField] private Transform Turret;
    [SerializeField] private GameObject R35Bullet;
    [SerializeField] private Transform FiringPosition;
    [SerializeField] private float BulletSpeed = 4.0f;

    //private void OnValidate()
    //{
    //    if(gameObject.tag == "LocalPlayer")
    //    {
    //        SerializedObject s = new SerializedObject(target as PlayerController);
    //        EditorGUILayout.PropertyField(RotationSpeed);
    //        EditorGUILayout.PropertyField(Acceleration)
    //        EditorGUILayout.PropertyField(Deceleration)
    //        EditorGUILayout.PropertyField(MaxSpeed)
    //        EditorGUILayout.PropertyField(TurretSpeed)
    //        EditorGUILayout.PropertyField(FiringRateDelay)
    //    }
    //    else if(gameObject.tag == "AI")
    //    {

    //    }
    //}

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
        CurrentSpeed = Vector3.zero;
    }

    void Update()
    {
        if (characterType == CharacterType.Player)
        {
            MousePos = Input.mousePosition;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Fire1") && FiringRateTime >= FiringRateDelay)
            {
                Shoot(R35Bullet, FiringPosition, BulletSpeed);
                FiringRateTime = 0f;
            }

            FiringRateTime += Time.deltaTime;
        }
        else if(characterType == CharacterType.AI)
        {
            distance = Vector2.Distance(transform.position, Player.transform.position);
            Vector2 direction = Player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distance < distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * (angle - 90.0f));
            }
        }
    }

    void FixedUpdate()
    {
        if (characterType == CharacterType.Player)
        {
            Rotation = movement.x * -RotationSpeed * Time.deltaTime;

            if (movement.y != 0)
            {
                CurrentSpeed += new Vector3(0, movement.y * Acceleration * Time.fixedDeltaTime, 0);
                if (CurrentSpeed.y > MaxSpeed)
                    CurrentSpeed.y = MaxSpeed;
            }
            else
            {
                CurrentSpeed.y = Mathf.Lerp(CurrentSpeed.y, 0, Deceleration * Time.deltaTime);
            }
        }
    }

    private void LateUpdate()
    {
        if (characterType == CharacterType.Player)
        {
            UpdateTurretRotation();
            transform.Translate(CurrentSpeed * Acceleration * Time.deltaTime);
            transform.Rotate(0f, 0f, Rotation);
        }
    }

    private void UpdateTurretRotation()
    {
        if (characterType == CharacterType.Player)
        {
            Vector2 ObjectPos = Camera.main.WorldToScreenPoint(Turret.position);
            MousePos = MousePos - ObjectPos;
            float angle = Mathf.Atan2(MousePos.y, MousePos.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            Turret.rotation = Quaternion.RotateTowards(Turret.rotation, target, Time.deltaTime * TurretSpeed);
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
