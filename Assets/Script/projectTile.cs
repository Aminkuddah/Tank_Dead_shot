using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectTile : MonoBehaviour
{
    public float speed;
    // public float lifeTime;

    public GameObject destroyEffect;
    public GameObject playerObject;

    private Transform player;
    private Transform wall;
    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectTile();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            DestroyProjectTile();
            kill();
        }
        if (other.CompareTag("Wall"))
        {
            DestroyProjectTile();
        }
    }

    private void kill(){
        Destroy(playerObject);
    }

    void DestroyProjectTile(){
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
