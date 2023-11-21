using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ScriptableObject")] public class PlayerDatas : ScriptableObject
{
    [FormerlySerializedAs("Difficulty")] [Header("LEVEL")]
    public Scene difficulty;
    [FormerlySerializedAs("NombreTourelles")] public int nombreTourelles;

    [FormerlySerializedAs("MaxLifePoint")] [Header("STATS TANK")] 
    public float maxLifePoint;
    [FormerlySerializedAs("LifePoint")] public float lifePoint;
    [FormerlySerializedAs("MaxBullet")] public float maxBullet;
    [FormerlySerializedAs("NbBullet")] public float nbBullet;

}
