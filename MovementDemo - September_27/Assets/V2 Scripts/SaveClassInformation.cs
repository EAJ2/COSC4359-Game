using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveClassInformation
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/PlayerInformation.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        V2PlayerData data = new V2PlayerData(player);
        stream.Close();
    }

    public static V2PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/PlayerInformation.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            V2PlayerData data = formatter.Deserialize(stream) as V2PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void DeleteSave(int choice)
    {
        if (choice == 1)
        {
            string path = Application.persistentDataPath + "/player1.fun";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
