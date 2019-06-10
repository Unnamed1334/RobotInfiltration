using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour {

    public GameObject bullet;

    public float cooldown;
    public float maxCooldown;

	// Use this for initialization
	void Start () {
        float difficulty = GameObject.Find("Difficulty").GetComponent<Difficulty>().difficulty;
        if (difficulty != 0) {
            if (difficulty < 1) {
                maxCooldown *= difficulty;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (cooldown <= 0 && Input.GetMouseButton(0)) {
            Instantiate(bullet, transform.position, transform.rotation);
            cooldown = maxCooldown;
        }
        if(cooldown >= 0) {
            cooldown -= Time.deltaTime;
        } 
	}
}
