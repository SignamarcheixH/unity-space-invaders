using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public enum States {
        wait, play, levelup, dead
    }
    public static States state;

    public int score;
    public int lives;
    public int wave;
    public Text scoreTxt;
    public Text livesTxt;
    public Text waveTxt;
    
    GameObject player;
    public GameObject enemyWave;
    public GameObject boom;
    public GameObject waitToStart;
    
    Camera cam;
    float height, width;

    void Start() {
        player = GameObject.FindWithTag("Player");
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
        waitToStart.gameObject.SetActive(true);
        int highscore = PlayerPrefs.GetInt("highscore");
        state = States.wait; 
    }

    public void HS_LaunchGame() {
        waitToStart.gameObject.SetActive(false);
        player.SetActive(true);
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemys) {
            Destroy(enemy);
        }
        HS_InitGame();
        HS_LoadLevel();
    }

    void HS_createNewWave() {
        float x = Random.Range(-width, 2 * (width / 5));
        float y = Random.Range(height / 2, height -1); //upper part of the screen
        Instantiate(enemyWave, new Vector2(x,y), Quaternion.identity);
    }

    void HS_LoadLevel() {
        state = States.play;
        HS_UpdateTexts();
    }

    void HS_InitGame() {
        score = 0;
        wave = 1;
        lives = 5;
    }

    void HS_UpdateTexts() {
        scoreTxt.text = "SCORE : " + score;
        waveTxt.text = "WAVE : " + wave;
        livesTxt.text = "LIVES : " + lives;
    }

    public void HS_AddScore(int points) {
        score += points;
        HS_UpdateTexts();
    }

    void Update() {
        if(state == States.play) {
            HS_UpdateTexts();
            HS_WaitForEndOfWave();
        }
    }

    void HS_WaitForEndOfWave() {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemys.Length == 0) { // when no more enemies in the current wave, start a new one
            wave += 1;
            HS_createNewWave();
        }
    }


    public void HS_KillPlayer() {
        StartCoroutine(HS_PlayerAgain());
    }

    IEnumerator HS_PlayerAgain() {
        state = States.dead;
        GameObject boomGO = Instantiate(boom, player.transform.position, Quaternion.identity);
        lives -= 1;
        player.SetActive(false);
        HS_UpdateTexts();
        if(lives <= 0) {
            yield return new WaitForSecondsRealtime(2f);
            Destroy(boomGO);
            HS_GameOver();
        }
        else {
            yield return new WaitForSecondsRealtime(3f);
            Destroy(boomGO);
            player.SetActive(true);
            state = States.play;
        }
    }

    void HS_GameOver() {
        state = States.wait;
        int highscore = PlayerPrefs.GetInt("highscore");
        if(score > highscore) {
            PlayerPrefs.SetInt("highscore", score);
        }
        waitToStart.gameObject.SetActive(true);
    }
}
