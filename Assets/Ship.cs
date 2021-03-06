﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public float speed = 1f;
    public float x;
    GameManager gameManager;
    public GameObject projectile;
    readonly float projectileSpeed = 16f;
    readonly float fireRate = .25f;
    float nextFire;
    Rigidbody2D rb;
    Camera cam;
    float height;
    float width;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        if(GameManager.state == GameManager.States.play) {
            HS_Move();
            HS_Fire();            
        }
    }

    void HS_Fire() {
        GameObject bullet = GameObject.FindWithTag("Bullet");
        if (!bullet) {
            if(Input.GetButton("Fire1") && nextFire > fireRate) {
                HS_Shoot();
                nextFire = 0;
            }
        }
        nextFire += Time.deltaTime;
    }

    void HS_Shoot() {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, projectileSpeed, 0);
    }


    void HS_Move() {
        float direction = Input.GetAxisRaw("Horizontal");
        x = 0;
        if(direction != 0.0) {
            x = direction < 0 ? -speed/4 : speed/4;
        }

    }

    private void FixedUpdate() {
        if ((transform.position.x + x > width - 1 ) || (transform.position.x + x < -width + 1)){
            x = 0;
        }
        transform.position = new Vector3(transform.position.x + x, transform.position.y , transform.position.z);     
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "EnemyBullet") {
            gameManager.HS_KillPlayer();
        }
        if(collision.tag == "LifeBonus") {
            gameManager.lives += 1;
            Destroy(collision.gameObject);
        }
        if(collision.tag == "PointBonus") {
            gameManager.HS_AddScore(50);
            Destroy(collision.gameObject);
        }
    }
}
