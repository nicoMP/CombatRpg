using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using EnemyBehaviour;

public class CyclopController : MonoBehaviour
{
    public float inX = 0, inY = 0;
    public bool canMove = true;
    public int orientation;
    public float disToChase = 1.5f;
    
    EnemyMovement move;
    PlayerRelation pRel;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;

    
    
    // Start is called before the first frame update
    void Start()
    {
        move = this.gameObject.AddComponent(typeof(EnemyMovement)) as EnemyMovement;
        pRel = this.gameObject.AddComponent(typeof(PlayerRelation)) as PlayerRelation;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.speed = 0.5f;
        
    }

    

    void FixedUpdate(){
        
        if(pRel.player != null){
            SetOrientationInt(-pRel.coorDisToPlayer.x,-pRel.coorDisToPlayer.y);
            movementInput = GetInput();
            if (canMove && movementInput != Vector2.zero){
                move.Move(movementInput);
            }else {
            animator.SetBool("isMoving", false);//no movement input so isnt moving 
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "PlayerWeapon"){
            Destroy(this.gameObject);
        }
    }

    private Vector2 GetInput(){

        Vector2 input = new Vector2 (0,0);

        if(pRel.lineDisToPlayer < disToChase){
            if(pRel.coorDisToPlayer.x > 0){
                input.x = -1;
            } else if(pRel.coorDisToPlayer.x < 0){
                input.x = 1;
            }
             if(pRel.coorDisToPlayer.y > 0){
                input.y = -1;
            } else if(pRel.coorDisToPlayer.y < 0){
                input.y = 1;
            }
       
        }
        return input;
    }

    private void SetOrientationInt(float x, float y){


         if(Math.Abs(y) > Math.Abs(x)){
            if(y > 0){
                orientation = 2;
            } else if (y < 0){orientation = 0;}
        }
        else if(Math.Abs(y) < Math.Abs(x)){
            if(x > 0){
                orientation = 3;
            } else if (x < 0) {orientation = 1;}
        }
        animator.SetInteger("lastOrientation", orientation);
        inX = x;
        inY = y;
    }
}
