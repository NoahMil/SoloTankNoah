using System.IO;
using UnityEngine;

namespace Memento
{
    public static class SaveGameManager
    {
        public static SaveSystem CurrentSaveData = new SaveSystem();
        public const string SaveDirectory = "/SaveData/";
        public const string FileName = "SaveGame.sav";

        public static void SaveGame()
        {
            var dir = Application.streamingAssetsPath + SaveDirectory;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonUtility.ToJson(CurrentSaveData, true);
            File.WriteAllText(dir + FileName, json);

            GUIUtility.systemCopyBuffer = dir;
        
            Debug.Log("Save effectuée !");
        }

        public static void LoadGame()
        {
            string fullPath = Application.streamingAssetsPath + SaveDirectory + FileName;
            SaveSystem tempData = new SaveSystem();

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveSystem>(json);
                Debug.Log("Load effectué !");

            }
            else
            {
                Debug.Log("Save file does not exist!");
            }
            CurrentSaveData = tempData;
        }
    }
}
