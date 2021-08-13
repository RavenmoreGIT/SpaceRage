using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 1f;

    public float RotationSpeed = 10f;

    public float MaxRotationAngle = 60f;

    public float ResetSpeed = 10f;

    public float MaximumAcceleration = 5;

    public float AccelerationRate = 1;
    public float LevelLimit = 7.5f;

    private float Acceleration = 0;
    
    private enum Direction{None,Left,Right};

    private Direction DirectionVariable = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion currentRotation = transform.rotation;
        if(Input.GetKey(KeyCode.A))
        {
            if (DirectionVariable != Direction.Left) Acceleration = 0;
            DirectionVariable = Direction.Left;
            transform.position += Time.deltaTime * (MovementSpeed+Acceleration) * Vector3.left;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, MaxRotationAngle, 0), Time.deltaTime*RotationSpeed);
            if (DirectionVariable == Direction.Left) Acceleration += AccelerationRate*Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D))
        {   if (DirectionVariable != Direction.Right) Acceleration = 0;
            DirectionVariable = Direction.Right;
            transform.position -= Time.deltaTime * (MovementSpeed+Acceleration) * Vector3.left;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -MaxRotationAngle, 0), Time.deltaTime*RotationSpeed);
            if (DirectionVariable == Direction.Right) Acceleration += AccelerationRate*Time.deltaTime;
        }
        else
        {
            Acceleration -= AccelerationRate * Time.deltaTime;
            DirectionVariable = Direction.None;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime*ResetSpeed);
        }

        Acceleration = Mathf.Clamp(Acceleration, 0,MaximumAcceleration);
        if (transform.position.x < -LevelLimit) transform.position = new Vector3(-LevelLimit, transform.position.y, transform.position.z);
        if (transform.position.x > LevelLimit) transform.position = new Vector3(LevelLimit, transform.position.y, transform.position.z);
    }
}
