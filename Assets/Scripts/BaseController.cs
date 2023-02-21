using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class Base_Controller : MonoBehaviour
{
    [SerializeField] protected AppDatas AppData;
    [SerializeField] private PlayerDatas _playerDatas;
    [SerializeField] protected int LifePoint;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] protected Transform BulletSpawnPosition;
    [SerializeField] private Transform TurretHead;
    [SerializeField] private Transform temp;
    [SerializeField] private bool IsAlreadyFiring;



    protected void Fire()
    {
        if (!IsAlreadyFiring)
        {
            IsAlreadyFiring = true;
            StartCoroutine(fireDelay());
        }
    }

    IEnumerator fireDelay()
    {
        Instantiate(BulletPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);   
        yield return new WaitForSeconds(2f);
        IsAlreadyFiring = false;
    }
    
    protected void RotateToTarget(Vector3 targetPos)
    {
        temp.position = new Vector3(targetPos.x, TurretHead.position.y, targetPos.z);
        TurretHead.transform.LookAt(temp);
    }

    public virtual void ApplyDamage(int damage)
    {
        LifePoint -=  damage;
        if (LifePoint <= 0)
        {
            Destruction();
        }
    }

    protected virtual void Destruction()
    {
        Destroy(gameObject);
    }

}
