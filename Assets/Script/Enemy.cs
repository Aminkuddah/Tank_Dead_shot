using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health = 5;
    public float stoppingDistance;
    public float retreatDistance;
    public float startShots;
    private float timeShots;
    public Text skorText;
    private int totalSkor = 0;
    // private int tampung = 0;

    // private int countStars = 0;
    
    private Transform player;
    public GameObject projectTile;
    public GameObject destroyEffect;
    public GameObject bulletStart;

    SpriteRenderer spriteRenderer;
    // private UnityEngine.Object enemyRef;
    private Material matDefault;
    private LivesEnemy livesEnemy;
    

    // Start is called before the first frame update
    void Start()
    {
        // enemyRef = ;
        spriteRenderer = GetComponent<SpriteRenderer>();
        livesEnemy = GetComponent<LivesEnemy>();
        matDefault = spriteRenderer.material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeShots = startShots;
        totalSkor = PlayerPrefs.GetInt("score");
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

        if (health <= 0)
        {
            health = 5;
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

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
