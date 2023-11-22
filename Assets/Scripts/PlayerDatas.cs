using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ScriptableObject")] public class PlayerDatas : ScriptableObject
{ 
    [Header("LEVEL")]
    public Scene difficulty; 
    public int nombreTourelles;
    
    [Header("STATS TANK")] 
    public float maxLifePoint; 
    public float lifePoint;

}
