using System.Collections;
using UnityEngine;

public class Tank : BaseController
{
    [SerializeField] private PlayerDatas playerData;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float boostSpeed = 0.1f;
    [SerializeField] private float boostDuration = 0.1f;
    private bool isBoosted = false;

    public delegate void TankEvents();
    public static event TankEvents OnUpdateHealth;

    private void Start()
    {
        if (appData.inFirstScene)
        {
            playerData.lifePoint = playerData.maxLifePoint;
        }

        OnUpdateHealth?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0f, 0f, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0f, 0f, -speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }

        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoostSpeed") && !isBoosted)
        {
            isBoosted = true;
            StartCoroutine(Boost());
        }
    }

    private IEnumerator Boost()
    {
        float tempSpeed = speed;
        speed = boostSpeed;
        yield return new WaitForSeconds(boostDuration);
        speed = tempSpeed;
        isBoosted = false;
    }

    public override void ApplyDamage(int damage)
    {
        playerData.lifePoint -= damage;
        if (playerData.lifePoint <= 0)
        {
            Destruction();
        }
        OnUpdateHealth?.Invoke();
    }
}