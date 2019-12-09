using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColor : MonoBehaviour
{

    public Color color1;
    public Color color2;
    private float duration = 3.0f;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {

        //cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;

    }

    // Update is called once per frame
    void Update()
    {

        float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = Color.Lerp(color1, color2, t);
        
    }
}
