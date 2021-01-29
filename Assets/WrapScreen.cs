using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapScreen : MonoBehaviour
{
    Camera cam;

    float height;
    float width;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;    
    }
}
