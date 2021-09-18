using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    public int HitPoints;
    public GameObject DeathEffect;

    private int HitPointsLeft;
    // Start is called before the first frame update
    void Start()
    {
        HitPointsLeft = HitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int Damage)
    {
        HitPointsLeft -= Damage;
        if (HitPointsLeft<0)
        {
            GameObject.Instantiate(DeathEffect,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
