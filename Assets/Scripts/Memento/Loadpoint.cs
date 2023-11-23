using UnityEngine;
using UnityEngine.Serialization;

namespace Memento
{
    public class Loadpoint : MonoBehaviour
    {
        [FormerlySerializedAs("PlayerSaveData")] public SaveData saveData;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                saveData.GeneralLoad();
            }
        }
    }
}