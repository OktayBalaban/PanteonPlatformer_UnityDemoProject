using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public bool isPainted;

    // Start is called before the first frame update
    void Start()
    {
        isPainted = false;
    }

    public void paintRed()
    {
        if (!isPainted)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

    }

}
