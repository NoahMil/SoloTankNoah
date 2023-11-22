using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSaveData : MonoBehaviour
{
    [SerializeField] private PlayerDatas playerData;
    private PlayerData myData = new PlayerData();

    public MonsterAI MonsterAI;
    
    public delegate void TankEvents();
    public static event TankEvents OnUpdateHealth;

    private void Update()
    {
        var transform1 = transform;
        myData.PlayerPosition = transform1.position;
        myData.PlayerRotation = transform1.rotation;
        myData.PlayerLifePoint = playerData.lifePoint;

        myData.TurretPosition = MonsterAI.transform.position;
        myData.TurretLifePoint = MonsterAI.lifePoint;

        
        if (Input.GetKeyDown(KeyCode.S))
        {
            GeneralSave();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GeneralLoad();
        }
    }
    
    public void GeneralSave()
    {
        SaveGameManager.CurrentSaveData.PlayerData = myData;
        SaveGameManager.SaveGame();
    }

    public void GeneralLoad()
    {
        SaveGameManager.LoadGame();
        myData = SaveGameManager.CurrentSaveData.PlayerData;
        var transform2 = transform;
        transform2.position = myData.PlayerPosition;
        transform2.rotation = myData.PlayerRotation;
        playerData.lifePoint = myData.PlayerLifePoint;

        MonsterAI.transform.position = myData.TurretPosition;
        MonsterAI.lifePoint = myData.TurretLifePoint;
        OnUpdateHealth?.Invoke();
    }
}

[System.Serializable]
public struct PlayerData
{
    [Header("TANK")]
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public float PlayerLifePoint;
    
    [Header("TURRET")]
    public Vector3 TurretPosition;
    public float TurretLifePoint;
    
}
