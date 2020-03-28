using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPlayer : MonoBehaviour
{
    // public Transform shootPoint;
    public GameObject glasses;
    public GameObject player;
    public GameObject bulletPrfab;
    public GameObject bulletStart;
    private Vector3 target;

    public float bulletSpeed = 10.0f;

    void Start(){
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GameIsPaused == true)
        {
            Cursor.visible = true;
        }
        else{
            target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            glasses.transform.position = new Vector2(target.x, target.y);
            Vector3 difference = target - player.transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0f, 0.0f, rotZ-90);
            if (Input.GetMouseButtonDown(0))
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                shoot(direction, rotZ);
            }
            Cursor.visible = false;
        }
    }

    void shoot(Vector2 direction, float rotZ){
        GameObject bullet = Instantiate(bulletPrfab) as GameObject;
        bullet.transform.position = bulletStart.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ-90);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
