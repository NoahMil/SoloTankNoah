using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _HealthBar;
    [SerializeField] private PlayerDatas _playerDatas;
    [SerializeField] private Bullet[] _BulletBar;


    
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
        _HealthBar.fillAmount = _playerDatas.LifePoint / _playerDatas.MaxLifePoint;
    }
    
    private void UpdateBullet()
    {
        for (int i = 0; i < _BulletBar.Length; i++)
        {
            if (_playerDatas.NbBullet <= _playerDatas.MaxBullet)
            {
                Destroy(_BulletBar[i]);
            }
        }
    }

    
    
}