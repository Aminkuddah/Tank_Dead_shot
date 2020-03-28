using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveDataEnemy(){
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string pathEnemy = Application.persistentDataPath + "/DataSaveEnemy.data";

        FileStream streamEnemy = new FileStream(pathEnemy, FileMode.Create);
        
        DataEnemy dataEnemy = new DataEnemy();

        binaryFormatter.Serialize(streamEnemy, dataEnemy);
        streamEnemy.Close();
    }

    public static void SaveDataPlayer(PlayerScript playerScript){
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string pathPlayer = Application.persistentDataPath + "/DataSavePlayer.data";

        FileStream streamPlayer = new FileStream(pathPlayer, FileMode.Create);

        DataPlayer dataPlayer = new DataPlayer(playerScript);

        binaryFormatter.Serialize(streamPlayer, dataPlayer);
        streamPlayer.Close();
    }

    public static DataEnemy LoadDataEnemy(){
        string path = Application.persistentDataPath + "/DataSaveEnemy.data";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataEnemy dataEnemy = binaryFormatter.Deserialize(stream) as DataEnemy;
            stream.Close();

            return dataEnemy;
        }else{
            Debug.Log("Data file tidak dapat ditemukan pada " + path);
            return null;
        }
    }

    public static DataPlayer LoadDataPlayer(){
        string path = Application.persistentDataPath + "/DataSavePlayer.data";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataPlayer dataPlayer = binaryFormatter.Deserialize(stream) as DataPlayer;
            stream.Close();

            return dataPlayer;
        }else{
            Debug.Log("Data file tidak dapat ditemukan pada " + path);
            return null;
        }
    }
}
