using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Velocity = 10;

    public int Damage = 1;

    public GameObject HitEffect;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity*Time.deltaTime*Vector3.up;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        HealthCounter hc = collision.gameObject.GetComponent<HealthCounter>();
        hc.Hit(Damage);
        Instantiate(HitEffect, transform.position, quaternion.identity);
        Destroy(this.gameObject);
    }
}
