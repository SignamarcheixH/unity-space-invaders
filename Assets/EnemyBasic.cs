using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

    GameManager gameManager;
    Rigidbody2D rb;
    public GameObject projectile;
    readonly float projectileSpeed = 8f;
    public float x;
    public float speed;

    Camera cam;

    float height;
    float width;

    bool toTheRight;


    public int points = 10;


    void HS_initEnemyStats() {
        speed = 1f;
        toTheRight = true;
    }

    void Start() {
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        HS_initEnemyStats();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        HS_Fire();
        HS_Move();
    }

    void HS_Move() {
        x = toTheRight ? speed/8 : -speed/8;
        if ((transform.position.x + x > width - 1 ) || (transform.position.x + x < -width + 1)) {
            x = 0;
            toTheRight = !toTheRight;
        }
        rb.MovePosition(new Vector3(transform.position.x + x, transform.position.y , transform.position.z));
    }

    public void HS_Fire() {
        float randomness = Random.Range(1, 1000);
        if(randomness >= 990) {
            GameObject bullet = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y - 1 , transform.position.z), transform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, -projectileSpeed, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            gameManager.HS_KillPlayer();
        }
        else if(collision.tag == "Bullet") {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            gameManager.HS_AddScore(points);
        }
        else if(collision.tag == "Enemy") {
            // Do nothing, but needed to prevent enemies to collide with other enemies
        } 
    }
}
