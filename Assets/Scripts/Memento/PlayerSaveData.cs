using UnityEngine;

public class PlayerSaveData : MonoBehaviour
{
    private PlayerData MyData = new PlayerData();
    public int lifePoint = 10;

    private void Update()
    {
        var transform1 = transform;
        MyData.PlayerPosition = transform1.position;
        MyData.PlayerRotation = transform1.rotation;
        MyData.LifePoint = lifePoint;
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGameManager.CurrentSaveData.PlayerData = MyData;
            SaveGameManager.SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("test");
            SaveGameManager.LoadGame();
            MyData = SaveGameManager.CurrentSaveData.PlayerData;
            transform.position = MyData.PlayerPosition;
            transform.rotation = MyData.PlayerRotation;
            lifePoint = MyData.LifePoint;
        }
    }
}

[System.Serializable]
public struct PlayerData
{
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public int LifePoint;
}
