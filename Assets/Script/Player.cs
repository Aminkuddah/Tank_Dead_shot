using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    
    private Rigidbody2D rb2D;
    private Vector2 moveVelocity;    

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
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
}
