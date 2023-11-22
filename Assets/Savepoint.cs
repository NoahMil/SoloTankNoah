using UnityEngine;

public class Savepoint : MonoBehaviour
{
    public PlayerSaveData PlayerSaveData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSaveData.GeneralSave();
        }
    }
}
