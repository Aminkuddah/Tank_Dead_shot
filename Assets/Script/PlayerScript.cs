using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 0;
    
    private Rigidbody2D rb2D;
    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (LoadGame.IsLoad == true)
        {
            LoadPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = move.normalized * speed;
    }

    void FixedUpdate(){
        rb2D.MovePosition(rb2D.position + moveVelocity * Time.deltaTime);
    }

    public void SavePlayer(){
        SaveSystem.SaveDataPlayer(this);
    }

    public void LoadPlayer(){
        DataPlayer dataPlayer = SaveSystem.LoadDataPlayer();
        
        Vector2 position;
        position.x = dataPlayer.positionPlayer[0];
        position.y = dataPlayer.positionPlayer[1];
        transform.position = position;
    }
}
