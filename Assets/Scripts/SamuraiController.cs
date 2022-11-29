using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyBehaviour;

public class SamuraiController : MonoBehaviour
{
    public float inX = 0, inY = 0;
    public bool canMove = true;
    public int orientation;
    public float disToRun = 1f, disToAttack = 0.5f;
    System.Random rnd;
    EnemyMovement move;
    PlayerRelation pRel;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;

    
    
    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
        move = this.gameObject.AddComponent(typeof(EnemyMovement)) as EnemyMovement;
        move.moveSpeed = 0.3f;
        pRel = this.gameObject.AddComponent(typeof(PlayerRelation)) as PlayerRelation;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.speed = 0.3f;
        
    }

    

    void FixedUpdate(){
        
        if(pRel.player != null){
            move.moveSpeed = 0.3f;
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
        bool attacking = false;
        Vector2 input = new Vector2 (0,0);
         if( pRel.lineDisToPlayer < disToAttack ){
            attacking = true;
            move.moveSpeed = 3f;
            switch(rnd.Next(0,4)){
                case 0:
                input.y = 0;
                input.x = 1;
                break;
                case 1:
                input.y = 1;
                input.x = 0;
                break;
                case 2:
                input.y = 0;
                input.x = -1;
                break;
                case 3:
                input.y = -1;
                input.x = 0;
                break;
            }
            if (pRel.lineDisToPlayer < 0.2 && pRel.lineDisToPlayer > disToAttack){attacking = false;}
       
        } else if (attacking == false && pRel.lineDisToPlayer < disToRun){
            if(pRel.coorDisToPlayer.x > 0){
                input.x = 1;
            } else if(pRel.coorDisToPlayer.x < 0){
                input.x = -1;
            }
             if(pRel.coorDisToPlayer.y > 0){
                input.y = 1;
            } else if(pRel.coorDisToPlayer.y < 0){
                input.y = -1;
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
