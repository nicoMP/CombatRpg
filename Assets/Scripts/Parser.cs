using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
namespace Parser{
    public class JsonParser : MonoBehaviour
    {
        // Start is called before the first frame update
        private string location;
        static public string JsonToString(string path){
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome");
            }
        }

        // Open the file to read from.
        using (StreamReader sr = File.OpenText(path))
        {
            char[] trim = { ' ', '[', ']'};
            string s;
            string file = "";
            while ((s = sr.ReadLine()) != null)
            {
                file += s;
            }
            string trimmed = "";
            foreach (char c in file){
                if(!Array.Exists(trim, e => e == c)){
                    trimmed += c;
                }
            }
            return trimmed;
        }
        }
        static public List<(string name, int score)> JStringToObject(string jString){
            var tuple = new List<(string name, int score)>();
            string[] players = jString.Split(new String[] {"},{","{", "}"},StringSplitOptions.RemoveEmptyEntries);
            foreach(string player in players){
                tuple.Add(PlayerToTuple(player));
            }
            return tuple;
        }
        static private (string name, int score) PlayerToTuple(string player){
            String[] pArr = player.Split(',',StringSplitOptions.RemoveEmptyEntries);
            (string name,int score) playerVar = (pArr[0].Split(new char[] {'"', ':'}, StringSplitOptions.RemoveEmptyEntries)[1], int.Parse(pArr[1].Split(new char[] {'"', ':'}, StringSplitOptions.RemoveEmptyEntries)[1]));
            (string name, int score) pTuple = (playerVar.name, playerVar.score);
            return pTuple;
        }
    }
}
