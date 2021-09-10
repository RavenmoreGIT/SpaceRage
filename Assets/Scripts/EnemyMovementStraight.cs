using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementStraight : MonoBehaviour
{
    public float MovementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * (MovementSpeed) * Vector3.down;
    }
}
