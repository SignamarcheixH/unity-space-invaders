using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public enum States {
        wait, play, levelup, dead
    }
    public static States state;

    // int level;
    public int score;
    public int lives;

    // public Text levelTxt;
    //public Text messageTxt;
    public Text scoreTxt;
    public Text livesTxt;
    
    GameObject player;
    // public GameObject asteroid; // le grand prefab
    public GameObject enemyWave;
    public GameObject boom;
    public GameObject waitToStart; // panel


    // public GameObject networkPanel;
    // Network NetworkManager;
    
    Camera cam;
    float height, width;

    // Start is called before the first frame update
    void Start() {
        // NetworkManager = GetComponent<Network>();
        // networkPanel.gameObject.SetActive(true); //panel actif au debut

        // messageTxt.gameObject.SetActive(false);

        player = GameObject.FindWithTag("Player");

        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;

        waitToStart.gameObject.SetActive(true);

        int highscore = PlayerPrefs.GetInt("highscore");
        // if(highscore > 0) {
        //     messageTxt.text = "HIGHSCORE : " + highscore;
        //     messageTxt.gameObject.SetActive(true);
        // }
        state = States.wait; 
    }

    public void LaunchGame() {

        // networkPanel.gameObject.SetActive(false);
        waitToStart.gameObject.SetActive(false);

        // restaurer après une partie
        player.SetActive(true);
        // messageTxt.gameObject.SetActive(false);
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemys) {
            Destroy(enemy);
        }

        // lancer une partie
        InitGame();
        LoadLevel();
    }

    void createNewWave() {
        float x = Random.Range(-width, 2 * (width / 5));
        float y = Random.Range(height / 2, height -1); //upper part of the screen
        Instantiate(enemyWave, new Vector2(x,y), Quaternion.identity);
    }

    void LoadLevel() {
        state = States.play;
        // createNewWave();
        // for(int i = 0; i < 3; i++) {
        //     //Instantiate(asteroid, new Vector2(x, y), Quaternion.identity);
        // }
        UpdateTexts();
    }

    void InitGame() {
        // level = 1;
        score = 0;
        lives = 5;
    }

    void UpdateTexts() {
        // levelTxt.text = "LEVEL : " + level;
        scoreTxt.text = "SCORE : " + score;
        livesTxt.text = "LIVES : " + lives;
    }

    public void AddScore(int points) {
        score += points;
        UpdateTexts();
    }

    // Update is called once per frame
    void Update() {
        if(state == States.play) {
            UpdateTexts();
            WaitForEndOfWave();
        }
    }

    void WaitForEndOfWave() {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemys.Length == 0) { // when no more enemies in the current wave, start a new one
            createNewWave();
        }
    }

    IEnumerator NewWave() {
        // state = States.levelup;
        // afficher message "level up"
        // messageTxt.text = "LEVEL UP";
        // messageTxt.gameObject.SetActive(true);
        // marquer une pause
        yield return new WaitForSecondsRealtime(3f);
        // cacher le message
        // messageTxt.gameObject.SetActive(false);
        // level += 1;
        LoadLevel();
    }

    public void KillPlayer() {
        StartCoroutine(PlayerAgain());
    }

    IEnumerator PlayerAgain() {
        state = States.dead;

        GameObject boomGO = Instantiate(boom, player.transform.position, Quaternion.identity);
        
        lives -= 1;
        player.SetActive(false);
        UpdateTexts();
        if(lives <= 0) {
            yield return new WaitForSecondsRealtime(2f);
            Destroy(boomGO);
            GameOver();
        }
        else {
            yield return new WaitForSecondsRealtime(3f);
            Destroy(boomGO);
            player.SetActive(true);
            state = States.play;
        }
    }

    void GameOver() {
        state = States.wait;

        int highscore = PlayerPrefs.GetInt("highscore");
        if(score > highscore) {
            PlayerPrefs.SetInt("highscore", score);
            // messageTxt.text = "NEW HIGHSCORE : " + score;
        }
        else {
            // messageTxt.text = "GAME OVER\nHIGHSCORE : "+ highscore;
        }
        

        // NetworkManager.SendScore(score);
        // networkPanel.gameObject.SetActive(true);
        // messageTxt.gameObject.SetActive(true);
        waitToStart.gameObject.SetActive(true);
    }
}
