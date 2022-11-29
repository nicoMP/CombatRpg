using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float collisionOffset = 1f;
    public float inX = 0, inY = 0;
    public ContactFilter2D movementFilter;
    public bool canMove = true, isMeele = true, isPause = false;
    public int orientation;

    public static PlayerController control;
    public GameObject spear, shuriken, weapon;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    void Awake(){
        control = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        weapon = spear;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag =="Enemy"){
            RestartGame(); 
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementInput != Vector2.zero){
            bool success = TryMove(movementInput);
            if(!success){
                success = TryMove(new Vector2(movementInput.x, 0));
                if(!success){
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            } //so it glides if it can in x or y direction 
            animator.SetBool("isMoving", success);//returns movement if it can move including glides or false if input doesnt produce movement
        }else {
        animator.SetBool("isMoving", false);//no movement input so isnt moving 
      }

    }
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
        if(movementInput != Vector2.zero ){
            SetOrientationInt(movementInput.x, movementInput.y);
            animator.SetInteger("lastOrientation", orientation);
        }
        
    }
    void OnSwitchWeapon(){
        isMeele = !isMeele;
        if(isMeele == true){weapon = spear;}
        else { weapon = shuriken;}
    }

    void OnFire(){
        if(isPause){RestartGame();}
        if(canMove){
        animator.speed = 0.6f;
        animator.SetTrigger("Attack");
        }
    }

    public void Attack(){
        if(canMove == false){
            (Vector2 pos,int rot) weaponPlacement = GetWeaponPlacement();
            if (weaponPlacement.rot  ==  180){Instantiate(weapon, weaponPlacement.pos,Quaternion.Euler(0,180,weaponPlacement.rot));}
            else Instantiate(weapon, weaponPlacement.pos,Quaternion.Euler(0,0,weaponPlacement.rot));
        }
    }

    public void EndAttack(){
        animator.speed = 1f;
        if (isMeele){Destroy(GameObject.FindWithTag("PlayerWeapon"));}
    }
    private (Vector2 , int) GetWeaponPlacement(){
        Vector2 pos = this.transform.position;
        int rot = 0;
        switch(orientation){
            case 0:
                pos.x += 0;
                pos.y += -0.175f;
                rot = 180;
                break;
            case 1:
                pos.x += -0.175f;
                pos.y += -0.04f;
                rot = 90;
                break;
            case 2:
                pos.x += 0;
                pos.y += 0.15f;
                rot = 0;
                break;
            case 3:
                pos.x += 0.175f;
                pos.y += -0.04f;
                rot = 270;
                break;
            default:
                break;       
        }

        return (pos, rot);
    }

    public void LockMovement(){
        canMove = false;
    }
    public void UnlockMovement(){
        canMove = true;
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

  

    private bool TryMove(Vector2 direction){

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
    void RestartGame(){
        SceneManager.LoadScene(0);
        ResumeGame();
    }
    void PauseGame ()
    {
        Time.timeScale = 0;
    }
    void ResumeGame ()
    {
        Time.timeScale = 1;
    }

    
}
