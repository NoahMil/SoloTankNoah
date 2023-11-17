using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class SaveSystem : MonoBehaviour
{
    /*public string saveName = "SaveData_";
    [Range(0, 10)] public int saveDataIndex = 0;

    public void SaveData(string dataToSave)
    {
        if (WriteToFile(saveName + saveDataIndex, dataToSave))
        {
            Debug.Log("Successfully saved data");
        }
    }

    public string LoadData()
    {
        string data = "";
        if (ReadFromFile(saveName + saveDataIndex, out data))
        {
            Debug.Log("Successfully loaded data");
        }
        return data;
    }


    private bool WriteToFile(string name, string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, name);

        try
        {
            File.WriteAllText(fullPath, content);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Error saving to a file " + e.Message);
        }

        return false;
    }

    private bool ReadFromFile(string name, out string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, name);
        try
        {
            content = File.ReadAllText(fullPath);
        }
        catch (Exception e)
        {
            Debug.Log("Error when loading the file " + e.Message);
            content = "";
        }

        return false;
    }*/
    public static void SaveCharacter(Tank character, string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);

        CharacterMemento memento = character.SaveToMemento();

        formatter.Serialize(stream, memento);
        stream.Close();
    }

    public static void LoadCharacter(Tank character, string filePath)
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            CharacterMemento memento = (CharacterMemento)formatter.Deserialize(stream);
            stream.Close();

            character.RestoreFromMemento(memento);
        }
        else
        {
            Debug.LogError("Save file not found");
        }
    }
}



