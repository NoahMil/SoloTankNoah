using UnityEngine;

public class Loadpoint : MonoBehaviour
{
    public PlayerSaveData PlayerSaveData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSaveData.GeneralLoad();
        }
    }
}