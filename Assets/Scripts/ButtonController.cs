using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
     public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
    public void RestartGame(){
        SceneManager.LoadScene(0);
        ResumeGame();
    }
    public void Quit() {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
