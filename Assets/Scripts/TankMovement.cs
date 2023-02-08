using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public PlayerInfo info;
    public float RotationSpeed = 10.0f;
    public float Acceleration = 1.5f;
    public float Deceleration = 8.0f;
    public float MaxSpeed = 1.0f;
    public float TurretSpeed = 50.0f;
    public Transform Turret;


    private Vector3 CurrentSpeed;
    private float Rotation;
    private Vector2 movement;
    private Vector2 MousePos;


    private void Start()
    {
        CurrentSpeed = Vector3.zero;
    }

    void Update()
    {
        switch (info.playerType)
        {
            case PlayerType.Local:
                MousePos = Input.mousePosition;
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                break;
            case PlayerType.EnemyAI:
                break;
            case PlayerType.AllyAI:
                break;
            case PlayerType.Enemy:
                break;
            default:
                Debug.Log("Error, no such player type");
                break;
        }
    }

    void FixedUpdate()
    {
        Rotation = movement.x * - RotationSpeed * Time.deltaTime;

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

    private void LateUpdate()
    {
        UpdateTurretRotation();
        transform.Translate(CurrentSpeed * Acceleration * Time.deltaTime);
        transform.Rotate(0f, 0f, Rotation);
    }

    private void UpdateTurretRotation()
    {
        Vector2 ObjectPos = Camera.main.WorldToScreenPoint(Turret.position);
        MousePos = MousePos - ObjectPos;
        float angle = Mathf.Atan2(MousePos.y, MousePos.x) * Mathf.Rad2Deg;
        Quaternion target = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Turret.rotation = Quaternion.RotateTowards(Turret.rotation, target, Time.deltaTime * TurretSpeed);
    }
}
