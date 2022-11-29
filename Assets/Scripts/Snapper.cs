using UnityEngine;
using UnityEditor;


public static class Snapper{
    [MenuItem("Edit/Snap Selected Object/to Grid")]
    public static void snapObject(){


        foreach(GameObject go in Selection.gameObjects){
            go.transform.position = go.transform.position.Round(0);
        }

        Debug.Log("Snap the buttons ");
    }

    [MenuItem("Edit/Snap Selected Object/to Center")]
    public static void centerObject(){


        foreach(GameObject go in Selection.gameObjects){
            go.transform.position = go.transform.position.Increase(0);

        }

        Debug.Log("Center the buttons ");
    }
    [MenuItem("Edit/Snap Selected Object/away from Center")]
    public static void awayObject(){


        foreach(GameObject go in Selection.gameObjects){
            go.transform.position = go.transform.position.Increase(3);

        }

        Debug.Log("Expand the buttons ");
    }
    

    

    public static Vector3 Round(this Vector3 v, int x){
        v.x = Mathf.Round(v.x + x);
        v.y = Mathf.Round(v.y + x);
        v.z = Mathf.Round(0);
        return v;
    }
     public static Vector3 Increase(this Vector3 v, int x){
        v.x = Mathf.Round(v.x* x);
        v.y = Mathf.Round(v.y* x);
        v.z = Mathf.Round(0);
        return v;
    }

    

}