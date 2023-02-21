using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ScriptableObject")] public class PlayerDatas : ScriptableObject
{
    [Header("LEVEL")]
    public Scene Difficulty;
    public int NombreTourelles;

    [Header("STATS TANK")] 
    public float MaxLifePoint;
    public float LifePoint;
    public float MaxBullet;
    public float NbBullet;

}
