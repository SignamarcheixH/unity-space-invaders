using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour {
    
	public List<GameObject> enemies;
    public List<GameObject> enemyTypes;
    // public GameObject enemyBasic;
    // public GameObject enemyLaser;
    public GameObject bonusManager;
	
    public float dropX;
	public float dropY;

    void Start() {
    	SelectTypeWave();
    }

    void Update() {
        removeDestroyedEnemies();
        waitForLastEnemy();
    }

    void removeDestroyedEnemies() {
        enemies.RemoveAll(item => item == null);
    }

    void waitForLastEnemy() {
        if(enemies.Count == 1) { // here we keep in mind the position of the last enemy in the wave in order to make the bonus spawn
            dropX = enemies[0].transform.position.x;
            dropY = enemies[0].transform.position.y;
        }
        if(enemies.Count == 0) { // we make the bonus spawn
            Instantiate(bonusManager, new Vector2(dropX, dropY), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void SelectTypeWave() {
		for (int i = 0; i < 5; i++) {
            System.Random rnd = new System.Random();
        	transform.position = new Vector3(transform.position.x + 2, transform.position.y , transform.position.z);
			enemies.Add(Instantiate(enemyTypes[rnd.Next(enemyTypes.Count)], transform.position, Quaternion.identity));
        }
    }
}
