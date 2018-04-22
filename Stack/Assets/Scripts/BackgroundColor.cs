using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour {

    public Color[] color;
    public float duration = 3.0F;
    public float t;
    public int colorIndex = 0;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    void Update()
    {
        t = Mathf.PingPong(Time.time, duration) / duration;
        if(t > 0.999)
        {
            if(colorIndex == color.Length - 1)
            {
                colorIndex = 0;
            }
            colorIndex++;
        }
        cam.backgroundColor = Color.Lerp(color[colorIndex+1], color[colorIndex], t);
    }
}
