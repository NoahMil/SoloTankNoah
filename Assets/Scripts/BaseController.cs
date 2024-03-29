using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseController : MonoBehaviour
{
        [SerializeField] protected AppDatas appData;
        [SerializeField] private PlayerDatas playerDatas;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] protected internal Transform bulletSpawnPosition;
        [SerializeField] private Transform turretHead;
        [SerializeField] private Transform temp;
        private int lifePoint;
        private bool isAlreadyFiring;



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
    
    protected void RotateToTarget(Vector3 targetPos)
    {
        temp.position = new Vector3(targetPos.x, turretHead.position.y, targetPos.z);
        turretHead.transform.LookAt(temp);
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

}
