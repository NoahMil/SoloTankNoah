using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Base_Controller
{

    [SerializeField] private GameObject TurretTarget;
    [SerializeField] private float DetectionRange = 10f;
    

    private void Update()
    {
        RotateToTarget(TurretTarget.transform.position);
        if (CheckTargetDistance())
        {
            Fire();
        }
    }

    private bool CheckTargetDistance()
    {
        RaycastHit hit;
        if (Physics.Raycast(BulletSpawnPosition.position, BulletSpawnPosition.up, out hit, DetectionRange))
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


