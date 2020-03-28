using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataEnemy
{ 
    public string level;
    public int healthEnemy =20;
    public int point = 10;
    public float[] positionEnemy;

    Scene currentScene = SceneManager.GetActiveScene();

    public DataEnemy(Enemy enemy){
        level = currentScene.name;
        healthEnemy = PlayerPrefs.GetInt("health");
        point = PlayerPrefs.GetInt("score");

        Debug.Log("Point save Data enemy : "+ point);
        Debug.Log("Health save Data enemy : "+ healthEnemy);
        
        positionEnemy = new float[3];
        // positionEnemy[0] = enemy.transform.position.x;
        // positionEnemy[1] = enemy.transform.position.y;
        // positionEnemy[2] = enemy.transform.position.z;
        positionEnemy[0] = PlayerPrefs.GetFloat("enemyPos.x");
        positionEnemy[1] = PlayerPrefs.GetFloat("enemyPos.y");
        positionEnemy[2] = PlayerPrefs.GetFloat("enemyPos.z");
    }
    
}
