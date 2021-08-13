using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject Bullet;

    public float Cooldown = 5;

    private float CooldownCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (CooldownCounter == 0)
        {

            if (Input.GetKey(KeyCode.Space))
            {
                GameObject.Instantiate(Bullet,transform.position,Quaternion.identity);
                CooldownCounter = Cooldown;
            }
            
        }
        else
        {
            CooldownCounter -= Time.deltaTime;
            if (CooldownCounter <= 0) CooldownCounter = 0;
        }
    }
}
