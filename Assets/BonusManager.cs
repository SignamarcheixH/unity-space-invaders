using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour {
    
	public List<GameObject> bonuses;
	readonly float speed = 8f;


    void Start() {
        HS_selectBonusType();
        Destroy(gameObject);
    }

    void HS_selectBonusType() {
    	System.Random rnd = new System.Random();
    	GameObject bonus = Instantiate(bonuses[rnd.Next(bonuses.Count)], transform.position, Quaternion.identity);
    	bonus.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, -speed, 0);
    }
}
