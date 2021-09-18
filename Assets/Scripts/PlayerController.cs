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
    public float HorizontalLevelLimit = 7.5f;
    public float VerticalLevelLimit = 3f;

    public ParticleSystem ps;

    private float Acceleration = 0;
    
    private enum Direction{None,Left,Right,Up,Down};

    private Direction DirectionVariable = 0;
    // Start is called before the first frame update
    void Start()
    {
        ps.Stop();
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
        else if (Input.GetKey(KeyCode.W))
        {
            if (DirectionVariable != Direction.Up) Acceleration = 0;
            DirectionVariable = Direction.Up;
            transform.position -= Time.deltaTime * (MovementSpeed + Acceleration) * Vector3.down;
            if (DirectionVariable == Direction.Right) Acceleration += AccelerationRate * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (DirectionVariable != Direction.Down) Acceleration = 0;
            DirectionVariable = Direction.Down;
            transform.position -= Time.deltaTime * (MovementSpeed + Acceleration) * Vector3.up;
            if (DirectionVariable == Direction.Right) Acceleration += AccelerationRate * Time.deltaTime;
        }
        else
        {
            Acceleration -= AccelerationRate * Time.deltaTime;
            DirectionVariable = Direction.None;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime*ResetSpeed);
        }

        //clamp accelleration, don't let the player leave the game area
        Acceleration = Mathf.Clamp(Acceleration, 0,MaximumAcceleration);
        if (transform.position.x < -HorizontalLevelLimit) transform.position = new Vector3(-HorizontalLevelLimit, transform.position.y, transform.position.z);
        if (transform.position.x > HorizontalLevelLimit) transform.position = new Vector3(HorizontalLevelLimit, transform.position.y, transform.position.z);
        if (transform.position.y < -VerticalLevelLimit) transform.position = new Vector3(transform.position.x, -VerticalLevelLimit, transform.position.z);
        if (transform.position.y > VerticalLevelLimit) transform.position = new Vector3(transform.position.x, VerticalLevelLimit, transform.position.z);


    }

    void OnCollisionEnter2D(Collision2D c)
    {
        ps.Play();
        // force is how forcefully we will push the player away from the enemy.
        float force = 60;

        // If the object we hit is the enemy
            // Calculate Angle Between the collision point and the player
            Vector3 dir = new Vector2(c.contacts[0].collider.transform.position.x, c.contacts[0].collider.transform.position.y) - new Vector2(transform.position.x,transform.position.y);
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody2D>().AddForce(dir * force);
    }
}
