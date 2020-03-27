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
    public float stoppingDistance;
    public float retreatDistance;
    public float startShots;
    private float timeShots;
    public Text skorText;
    public int totalSkor = 0;
    public string level;
    
    private Transform player;
    public GameObject projectTile;
    public GameObject destroyEffect;
    public GameObject bulletStart;

    SpriteRenderer spriteRenderer;
    // private UnityEngine.Object enemyRef;
    private Material matDefault;
    private Scene currentScene;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeShots = startShots;
        totalSkor = PlayerPrefs.GetInt("score");
        currentScene = SceneManager.GetActiveScene();
        level = currentScene.name;
        if (LoadGame.IsLoad == true)
        {
            LoadEnemy();
        }else{
            if (level == "GameplayEasy")
            {
                speed = 2;
                health = 3;
            }
            else if(level == "GameplayMedium"){
                speed = 4;
                health = 5;
            }
            else if(level == "GameplayHard")
            {
                speed = 6;
                health = 8;
            }
        }
        
        Debug.Log("Kecepatan : "+speed);
        Debug.Log("Nyawa : " + health);
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
        }
    }

    void ResetMaterial(){
        spriteRenderer.material = matDefault;
    }

    private void kill(){
        GameObject e = Instantiate(destroyEffect) as GameObject;
        e.transform.position = transform.position;
        // Destroy(other.gameObject);
        spriteRenderer.enabled = false; 
        Invoke("Respawn", 1);
    }

    void Respawn(){
        GameObject clone = Instantiate(Resources.Load("enemy"), transform.position, Quaternion.identity) as GameObject;        
        Destroy(gameObject);
    }

    private void updateSkorText(){
        string msg = "Skor = " + totalSkor;
        Debug.Log("ini msg " + msg);
        skorText.text = msg;
    }

    public void SaveEnemy(){
        SaveSystem.SaveDataEnemy(this);
    }

    public void LoadEnemy(){
        DataEnemy dataEnemy = SaveSystem.LoadDataEnemy();
        
        health = dataEnemy.healthEnemy;
        totalSkor = dataEnemy.point;
        Debug.Log("Skorny adalah "+totalSkor);

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
