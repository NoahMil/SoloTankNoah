using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ScriptableObject")] public class AppDatas : ScriptableObject
{
    [FormerlySerializedAs("InFirstScene")] public bool inFirstScene = true;
}
