using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Base_Controller
{

    [SerializeField] private GameObject _turretTarget;
    [SerializeField] private float _detectionRange = 10f;
    

    private void Update()
    {
        RotateToTarget(_turretTarget.transform.position);
        if (CheckTargetDistance())
        {
            Fire();
        }
    }

    private bool CheckTargetDistance()
    {
        RaycastHit hit;
        if (Physics.Raycast(BulletSpawnPosition.position, BulletSpawnPosition.up, out hit, _detectionRange))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}


