using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EnemyBehaviour
{
        public class PlayerRelation : MonoBehaviour
    {
        public PlayerRelation playerR;
        public GameObject player;
        public GameObject enemy;
        public Vector2 coorDisToPlayer;
        public double lineDisToPlayer;

        void Start(){
                playerR = this;
                player = GameObject.FindWithTag("Player");
                enemy = this.gameObject;
        }

        void FixedUpdate(){
            if(player != null){setDistance(enemy.transform.position, player.transform.position);}
        }

        public void setDistance(Vector2 a, Vector2 b){
            coorDisToPlayer = new Vector2((a.x-b.x),(a.y-b.y));
            lineDisToPlayer = Math.Sqrt(coorDisToPlayer.x*coorDisToPlayer.x + coorDisToPlayer.y*coorDisToPlayer.y);
        }

    }

    public class EnemyMovement : MonoBehaviour
    {
        public float moveSpeed = 0.4f;
        public float collisionOffset = 0.05f; 
        public bool canMove = true;
        public int orientation;

        public Vector2 movementInput;
        public ContactFilter2D movementFilter;
        
        Animator animator;
        Rigidbody2D rb;
        List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        public bool TryMove(Vector2 direction){

            int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);
                
            if (count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else return false;

        }
        public void Move(Vector2 movementInput){
        bool success = TryMove(movementInput);
        if(!success){
            success = TryMove(new Vector2(movementInput.x, 0));
            if(!success){
                success = TryMove(new Vector2(0, movementInput.y));
            }
        } //so it glides if it can in x or y direction 
        animator.SetBool("isMoving", success);//returns movement if it can move including glides or false if input doesnt produce movement
    }
    }
        
}
