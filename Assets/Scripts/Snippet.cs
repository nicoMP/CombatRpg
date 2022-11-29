using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snippet : MonoBehaviour
{
    public void DestroyParent(){
        Destroy(transform.parent.gameObject);
    }
}
