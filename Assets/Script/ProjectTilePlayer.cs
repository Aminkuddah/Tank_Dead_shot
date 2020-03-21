using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectTilePlayer : MonoBehaviour
    {
    public float lifeTime;
    private Vector3 target;

    public GameObject destroyEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform.position;;
    }

    // Update is called once per frame
    void Update(){
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectTile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Enemy"))
        {
            DestroyProjectTile();
        }
        if (other.CompareTag("Wall"))
        {
            DestroyProjectTile();
        }
    }

    void DestroyProjectTile(){
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
