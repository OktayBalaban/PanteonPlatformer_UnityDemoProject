using UnityEngine;

public class Painter : MonoBehaviour
{
    public bool isPainted;

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
