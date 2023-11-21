using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Tank : BaseController
{
    [SerializeField] private PlayerDatas playerData;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float boostSpeed = 0.1f;
    [SerializeField] private float boostDuration = 0.1f;
    [SerializeField] private bool isBoosted = false;

    public Vector3 Position { get; set; }

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
       // AimToTarget();

        Position = transform.position;

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

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveCharacter();
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            LoadGame();
        }
    }

    /*
    private void AimToTarget()
    {
        Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(tempRay, out hit))
        {
            RotateToTarget(hit.point);
        }
    }
    */

    [FormerlySerializedAs("TurretTarget")] [SerializeField] private GameObject turretTarget;
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

    public CharacterMemento SaveToMemento()
    {
        return new CharacterMemento(transform.position, playerData.lifePoint);
    }

    public void RestoreFromMemento(CharacterMemento memento)
    {
        transform.position = memento.position;
        playerData.lifePoint = memento.health;
    }

    public void SaveCharacter()
    {
        SaveSystem.SaveCharacter(this, "save/file.sav");
    }

    private void LoadGame()
    {
        SaveSystem.LoadCharacter(this, "save/file.sav");
    }
}

[Serializable]
public class CharacterMemento
{
    public Vector3 position;
    public float health;

    public CharacterMemento(Vector3 position, float health)
    {
        this.position = position;
        this.health = health;
    }
}