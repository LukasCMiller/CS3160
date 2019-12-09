using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBox : MonoBehaviour
{

    public Material red;
    public Material black;

    private Renderer renderer;

    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        renderer.sharedMaterial = black;
    }

    private void OnMouseUp()
    {
        renderer.sharedMaterial = red;
    }
}
