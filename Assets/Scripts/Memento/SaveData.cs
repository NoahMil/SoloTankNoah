using System;
using State;
using UnityEngine;

namespace Memento
{
    public class SaveData : MonoBehaviour
    {
        [Header("PLAYER")]
        [SerializeField] private PlayerDatas _playerData;
        [SerializeField] private Tank Tank;
    
        [Header("TURRET")]
        [SerializeField] MonsterAI MonsterAI;
    
        private PlayerData _myData = new PlayerData();
        
        public delegate void TankEvents();
        public static event TankEvents OnUpdateHealth;
    
        private void Update()
        {
            var transform1 = Tank.transform;
            _myData.PlayerPosition = transform1.position;
            _myData.PlayerRotation = transform1.rotation;
            _myData.PlayerLifePoint = _playerData.lifePoint;

            _myData.TurretPosition = MonsterAI.transform.position;
            _myData.TurretLifePoint = MonsterAI.lifePoint;

        
            if (Input.GetKeyDown(KeyCode.S))
            {
                GeneralSave();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GeneralLoad();
            }
        }
    
        public void GeneralSave()
        {
            SaveGameManager.CurrentSaveData.PlayerData = _myData;
            SaveGameManager.SaveGame();
        }

        public void GeneralLoad()
        {
            SaveGameManager.LoadGame();
            _myData = SaveGameManager.CurrentSaveData.PlayerData;
            Tank.transform.position = _myData.PlayerPosition;
            Tank.transform.rotation = _myData.PlayerRotation;
            _playerData.lifePoint = _myData.PlayerLifePoint;

            MonsterAI.transform.position = _myData.TurretPosition;
            MonsterAI.lifePoint = _myData.TurretLifePoint;
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
}