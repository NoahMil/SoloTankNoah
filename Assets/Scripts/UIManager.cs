using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [FormerlySerializedAs("_HealthBar")] [SerializeField] private Image healthBar;
    [FormerlySerializedAs("_playerDatas")] [SerializeField] private PlayerDatas playerDatas;
    [FormerlySerializedAs("_BulletBar")] [SerializeField] private Bullet[] bulletBar;


    
    private void OnEnable()
    {
        Tank.OnUpdateHealth += UpdateHealthBar;
        Tank.OnUpdateHealth += UpdateBullet;

    }
    private void OnDisable()
    {
        Tank.OnUpdateHealth -= UpdateHealthBar;
        Tank.OnUpdateHealth -= UpdateBullet;

    }
    
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = playerDatas.lifePoint / playerDatas.maxLifePoint;
    }
    
    private void UpdateBullet()
    {
        for (int i = 0; i < bulletBar.Length; i++)
        {
            if (playerDatas.nbBullet <= playerDatas.maxBullet)
            {
                Destroy(bulletBar[i]);
            }
        }
    }

    
    
}