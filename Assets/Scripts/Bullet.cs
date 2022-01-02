using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Velocity = 10;
    public float Acceleration = 0;
    public float AccelerationDecay = 0;
    public float Lifetime=5;
    public int Damage = 1;

    public GameObject HitEffect;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity*Time.deltaTime*Vector3.up;
        Velocity += Acceleration * Time.deltaTime;
        Acceleration -= AccelerationDecay * Time.deltaTime;
        Acceleration = Mathf.Max(0, Acceleration);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name != "PlayerShip" && collision.transform.tag != "PlayerMissile" )
        {
            HealthCounter hc = collision.gameObject.GetComponent<HealthCounter>();
            if (hc) hc.Hit(Damage);
            Instantiate(HitEffect, transform.position, quaternion.identity);

            ParticleSystem[] parts = gameObject.GetComponentsInChildren<ParticleSystem>();
            if (parts.Length>0)
                foreach(ParticleSystem ps in parts)
                {
                    ps.gameObject.transform.parent = null;
                    var emission = ps.emission;
                    emission.enabled = false;

                }
            Destroy(this.gameObject);
        }
    }
}
