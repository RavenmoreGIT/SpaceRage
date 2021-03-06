using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveHorizontal;
    private float moveVertical;

    public float MovementSpeed = 1f;

  

    public ParticleSystem ps;
    public Animator PlayerAnimator;
    public TextMeshPro text;
    public Rigidbody2D PlayerRigidbody;

    public float xClamp = 5;
    public float yClamp = 5;

    private Vector2 movement;
    
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

        moveHorizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        moveVertical = Input.GetAxis("Vertical") * MovementSpeed;
        movement = new Vector2(moveHorizontal, moveVertical);
        PlayerRigidbody.velocity = movement;

        //limiting position
        transform.localPosition = new Vector2(Mathf.Clamp(transform.localPosition.x, -xClamp, xClamp),
            Mathf.Clamp(transform.localPosition.y, 0, yClamp)
            );



        //Animator handling.
        if (Input.GetKey(KeyCode.A))
        {
            PlayerAnimator.SetTrigger("Left");
        }

        if (Input.GetKey(KeyCode.D))
        {
            PlayerAnimator.SetTrigger("Right");
        }
        
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) PlayerAnimator.SetTrigger("None");

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
