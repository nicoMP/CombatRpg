using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.InputSystem;


public class GameController : MonoBehaviour
{
    public GameObject pauseButtons;
    public GameObject player;
    public Vector2 coorsDisToGoal;
    public double lineDisToGoal;
    bool isPause = false;
    bool notMenu;
    public bool leaderBoardActive;
    private PlayerInput inputSystem;
    AsyncOperation asyncLoad;
    void Start(){
        inputSystem = GetComponent<PlayerInput>();
        notMenu = SceneManager.GetActiveScene().buildIndex != 0;
        if(notMenu){
            player = GameObject.FindWithTag("Player");
            }
        
    }
    void FixedUpdate(){

       if(notMenu){
            SetDistance(player.transform.position);
            if(lineDisToGoal < 0.35){
                RestartGame();
            }
        }
    }
    void OnPause(){
        inputSystem.SwitchCurrentActionMap("UI");
        if(isPause){
            Destroy(GameObject.FindWithTag("PauseButton"));
            ResumeGame();

        }
        else { 
            PauseGame();
            Instantiate(pauseButtons);
            }
        isPause = !isPause;
        
    }
    public void RestartGame(){
        SceneManager.LoadScene(0);
        ResumeGame();
    }
    void PauseGame ()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
    public void SetDistance(Vector2 a){
            Vector2 b = new Vector2(5,0);
            coorsDisToGoal = new Vector2((a.x-b.x),(a.y-b.y));
            lineDisToGoal = Math.Sqrt(coorsDisToGoal.x*coorsDisToGoal.x + coorsDisToGoal.y*coorsDisToGoal.y);
        }

    public void ShowLeaderBoard(){
        if(leaderBoardActive != true){
            leaderBoardActive = true;
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
         // Wait until the asynchronous scene fully loads
      
        }
    }

    public void StartGame(){

        SceneManager.LoadScene(1);
    }

    public void LeaderBoardRemove(){
        leaderBoardActive = false;
    }
    public void Quit() {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    public void DestroyParent(){
        Destroy(transform.parent.gameObject);
    }
    public void ToggleInput(){
        
    }
}