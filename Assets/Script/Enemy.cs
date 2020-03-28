using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public int totalSkor;
    public float stoppingDistance;
    public float retreatDistance;
    public float startShots;
    private float timeShots;
    public Text skorText;
   
    public string level;
    
    private Transform player;
    public GameObject projectTile;
    public GameObject destroyEffect;
    public GameObject bulletStart;

    SpriteRenderer spriteRenderer;
    private Material matDefault;
    private Scene currentScene;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeShots = startShots;
        totalSkor = PlayerPrefs.GetInt("score");
        currentScene = SceneManager.GetActiveScene();
        level = currentScene.name;
        Debug.Log("IsLoad Enemy : " + LoadGame.IsLoad);
        
        if (level == "GameplayEasy")
        {
            speed = 2;
        }
        else if(level == "GameplayMedium"){
            speed = 4;
        }
        else if(level == "GameplayHard")
        {
            speed = 6;
        }

        if (LoadGame.IsLoad == true)
        {   
            LoadEnemy();
        }else{
            if (level == "GameplayEasy")
            {
                health = 3;
            }
            else if(level == "GameplayMedium"){
                health = 5;
            }
            else if(level == "GameplayHard")
            {
                health = 8;
            }
        }
        Debug.Log("Ini Speed = "+speed);
        Debug.Log("Ini Health = "+health);
    }

    // Update is called once per frame
    void Update()
    {
         if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        float rotZ = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0.0f, rotZ-90);
        if (timeShots <= 0)
        {
            GameObject bullet = Instantiate(projectTile) as GameObject;
            bullet.transform.position = bulletStart.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ-90);
            timeShots = startShots;
        }
        else
        {
            timeShots -= Time.deltaTime;
        }
        PlayerPrefs.SetFloat("enemyPos.x", transform.position.x);
        PlayerPrefs.SetFloat("enemyPos.y", transform.position.y);
        PlayerPrefs.SetFloat("enemyPos.z", transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            health--;

            Debug.Log(health);
            if (health <= 0)
            {
                totalSkor += 20;
                updateSkorText();
                kill();
            }
            else{
                Invoke("ResetMaterial", 0.1f);
                totalSkor += 10;
                updateSkorText();
            }
            PlayerPrefs.SetInt("score", totalSkor);
            PlayerPrefs.SetInt("health", health);
        }
    }

    void ResetMaterial(){
        spriteRenderer.material = matDefault;
    }

    private void kill(){
        GameObject e = Instantiate(destroyEffect) as GameObject;
        e.transform.position = transform.position;
        spriteRenderer.enabled = false; 
        Invoke("Respawn", 1);
    }

    void Respawn(){
        GameObject clone = Instantiate(Resources.Load("enemy"), transform.position, Quaternion.identity) as GameObject;        
        Destroy(gameObject);
        LoadGame.IsLoad = false;
    }

    private void updateSkorText(){
        string msg = "Skor = " + totalSkor;
        skorText.text = msg;
    }

    public void SaveEnemy(){
        SaveSystem.SaveDataEnemy(this);
    }

    public void LoadEnemy(){
        DataEnemy dataEnemy = SaveSystem.LoadDataEnemy();
        
        health = dataEnemy.healthEnemy;
        totalSkor = dataEnemy.point;

        string msg = "Skor = " + dataEnemy.point;
        skorText.text = msg;

        Vector3 position;
        position.x = dataEnemy.positionEnemy[0];
        position.y = dataEnemy.positionEnemy[1];
        position.z = dataEnemy.positionEnemy[2];
        transform.position = position;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
