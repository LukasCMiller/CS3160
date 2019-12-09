using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        
        //handle the left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Rect test = new Rect(0, 0, Screen.width, Screen.height);
            GUI.Label(test,"test");
        }
    }
}
