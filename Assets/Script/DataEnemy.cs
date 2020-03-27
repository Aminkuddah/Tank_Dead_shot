using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataEnemy
{ 
    public string level;
    public int healthEnemy;
    public int point;
    public float[] positionEnemy;

    Scene currentScene = SceneManager.GetActiveScene();

    public DataEnemy(Enemy enemy){
        level = currentScene.name;
        healthEnemy = enemy.health;
        point = enemy.totalSkor;

        positionEnemy = new float[3];
        positionEnemy[0] = enemy.transform.position.x;
        positionEnemy[1] = enemy.transform.position.y;
        positionEnemy[2] = enemy.transform.position.z;
    }
    
}
