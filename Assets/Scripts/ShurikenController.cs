using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShurikenController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    PlayerController pCon;
    Vector2 orientation;
    public float moveSpeed = 3f;
    Vector2 startPos;
    void Start (){
        startPos = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        pCon = player.GetComponent<PlayerController>();
        orientation = new Vector2(0,0);
        switch(pCon.orientation){
            case 0:
            orientation.y = -1;
            break;
            case 1:
            orientation.x = -1;
            break;
            case 2:
            orientation.y = 1;
            break;
            case 3:
            orientation.x = 1;
            break;
        }
    }
    void FixedUpdate(){
        double distance = Math.Sqrt((this.transform.position.x- startPos.x)*(this.transform.position.x- startPos.x)+(this.transform.position.y- startPos.y)*(this.transform.position.y- startPos.y));
        if(distance > 1){
            Destroy(this.gameObject);
        }
        rb.MovePosition(rb.position + orientation * moveSpeed * Time.fixedDeltaTime);
    }
}
