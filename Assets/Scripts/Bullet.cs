using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int _damage = 1;


    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up * speed;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<Base_Controller>() != null)
        {

            collision.gameObject.GetComponentInParent<Base_Controller>().ApplyDamage(_damage);
        }
        
        Destroy(gameObject);
    }
}
