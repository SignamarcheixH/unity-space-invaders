using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {
	Camera cam;
    float height;
    float width;

    void Start() {
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;    
    }

    void Update() {
        if(transform.position.y < -height + 1) {
            Destroy(gameObject);
        }
    }
}
