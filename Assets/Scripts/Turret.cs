using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : BaseController
{

    [FormerlySerializedAs("_turretTarget")] [SerializeField] private GameObject turretTarget;
    [FormerlySerializedAs("_detectionRange")] [SerializeField] private float detectionRange = 10f;
    

    private void Update()
    {
        RotateToTarget(turretTarget.transform.position);
        if (CheckTargetDistance())
        {
            Fire();
        }
    }

    private bool CheckTargetDistance()
    {
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawnPosition.position, bulletSpawnPosition.up, out hit, detectionRange))
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


