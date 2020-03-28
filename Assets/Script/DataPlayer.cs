using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataPlayer
{ 
    public float[] positionPlayer;
    public string level;
    Scene currentScene = SceneManager.GetActiveScene();

    public DataPlayer(PlayerScript playerScript){
        level = currentScene.name;
        
        positionPlayer = new float[2];
        positionPlayer[0] = playerScript.transform.position.x;
        positionPlayer[1] = playerScript.transform.position.y;
        Debug.Log("Posisi Player y: "+ positionPlayer[1]);
    }
    
}
