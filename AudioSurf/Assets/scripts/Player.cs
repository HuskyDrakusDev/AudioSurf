using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameManager gameManager;
    public int health;
    public int currentHealth;
    public int lives;
    public Material material;
    public string playerName;
    public float jumpValue;
    public int speed;
    public int distancer;
	// Use this for initialization
	void Start () {
		
	}
	//find position of points in real world space and just move the player based on that using the belzier thing online

	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * speed*Time.deltaTime;
        
      
    }
    public bool isDead(){

        return false;
    }
    public void Die(){

    }
    public void Jump(){

    }
}
