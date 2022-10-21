using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 10);
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.gameObject.CompareTag("Ground"))
            Destroy(this.gameObject);
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.SendMessage("TakeDamage", 1);
        }
        Destroy(this.gameObject);
    }
}
