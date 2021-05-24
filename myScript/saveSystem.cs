using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class saveSystem
{
    public static void SaveSystem(gameData gd)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameSave.bin";
        FileStream stream = new FileStream(path,FileMode.Create);
        playerData data = new playerData(gd);
        formatter.Serialize(stream,data);
        stream.Close();
    }

    public static playerData LoadSystem()
    {
        string path = Application.persistentDataPath + "/gameSave.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            playerData data = formatter.Deserialize(stream) as playerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
