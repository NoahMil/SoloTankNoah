using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [FormerlySerializedAs("_damage")] [SerializeField] private int damage = 1;


    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up * speed;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<BaseController>() != null)
        {

            collision.gameObject.GetComponentInParent<BaseController>().ApplyDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
