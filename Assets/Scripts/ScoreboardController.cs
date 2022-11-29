using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;
using System.IO;
using Parser;
using System;
using TMPro;

public class ScoreboardController : MonoBehaviour
{
    GameObject GC;
    AsyncOperation AS;
    public TMP_Text[] Spots = new TMP_Text[8];
    // Start is called before the first frame update
    void Start()
    {
        string l = JsonParser.JsonToString("Assets/LeaderBoard.json");
        var a = JsonParser.JStringToObject(l);
        (string name, int score)[] ranking = new (string name, int score)[a.Count];
        foreach(var it in a){
            var item = it;
            for(int i = 0; i < a.Count ; i++){
                if(ranking[i].score < item.score){
                    var temp = item;
                    item = ranking[i];
                    ranking[i] = temp;
                    
                }
            }
        }
        
        for (int i = 0, p = 0; i < Spots.Length; i++){
            if(p < a.Count){Spots[i].text =  ranking[p].name;
            Spots[++i].text = ranking[p++].score.ToString();}
        }

        GC = GameObject.Find("GameControllerMenu");
    }

    public void RemoveLeaderBoard(){
        GC.GetComponent<GameController>().LeaderBoardRemove();
        AS = SceneManager.UnloadSceneAsync(2);
    }
}
