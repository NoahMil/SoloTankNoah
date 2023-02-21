using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Tank : Base_Controller
{
    [SerializeField] private PlayerDatas _playerData;
    [SerializeField] private float Speed = 0.1f;
    [SerializeField] private float RotationSpeed = 0.1f;
    [SerializeField] private float BoostSpeed = 0.1f;
    [SerializeField] private  float BoostDuration = 0.1f;
    [SerializeField] private bool  IsBoosted = false ;

    public delegate void TankEvents();

    public static event TankEvents OnUpdateHealth;
    
    private void Start()
    {
        if (AppData.InFirstScene)
        {
            _playerData.LifePoint = _playerData.MaxLifePoint;
        }
        OnUpdateHealth?.Invoke();
    }

    private void Update()
    {
        AimToTarget();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0f, 0f, Speed*Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0f, 0f, -Speed*Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0f, RotationSpeed*Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0f, -RotationSpeed*Time.deltaTime, 0f);
        }
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }


    private void AimToTarget()
    {
        Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(tempRay,  out hit))
        {
            RotateToTarget(hit.point);
        } 
    }



    [SerializeField] private GameObject TurretTarget;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoostSpeed") && !IsBoosted)
        {
            IsBoosted = true;
            StartCoroutine(Boost());
        }
    }
    
    private IEnumerator Boost()
    {
        float tempSpeed = Speed;
        Speed = BoostSpeed;
        yield return new WaitForSeconds(BoostDuration);
        Speed = tempSpeed;
        IsBoosted = false;
    }
    
    public override void ApplyDamage(int damage)
    {

        _playerData.LifePoint -=  damage;
        if (_playerData.LifePoint <= 0)
        {
            Destruction();
        }
        OnUpdateHealth?.Invoke();

    }
}

