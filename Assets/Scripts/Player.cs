using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   /* [SerializeField] protected AppDatas appData;
    [SerializeField] private PlayerDatas playerDatas;
    [SerializeField] protected int lifePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] protected internal Transform bulletSpawnPosition;
    [SerializeField] private PlayerDatas playerData;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float boostSpeed = 0.1f;
    [SerializeField] private float boostDuration = 0.1f;
    [SerializeField] private bool isBoosted = false;
    private Animator marioAnimator;

    public bool isAlreadyFiring;

    private void Start()
    {
        marioAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0f, 0f, speed * Time.deltaTime);
            marioAnimator.SetBool("isRunning", true);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0f, 0f, -speed * Time.deltaTime);
            marioAnimator.SetBool("isRunning", true);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            marioAnimator.SetBool("isRunning", true);

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
            marioAnimator.SetBool("isRunning", true);

        }

        else
        {
            marioAnimator.SetBool("isRunning", false);
        }

        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    protected void Fire()
    {
        if (!isAlreadyFiring)
        {
            isAlreadyFiring = true;
            StartCoroutine(FireDelay());
        }
    }

    IEnumerator FireDelay()
    {
        Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);   
        yield return new WaitForSeconds(2f);
        isAlreadyFiring = false;
    }
    

    public virtual void ApplyDamage(int damage)
    {
        lifePoint -=  damage;
        if (lifePoint <= 0)
        {
            Destruction();
        }
    }

    protected virtual void Destruction()
    {
        Destroy(gameObject);
    }
    
    */


}

