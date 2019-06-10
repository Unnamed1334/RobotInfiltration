using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour {

    public int maxHealth = 200;
    public int health = 200;

    public GameObject healthBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage) {
        if (health > 0) {
            health -= damage;
            if (healthBar != null) {
                if (health <= maxHealth) {
                    healthBar.SetActive(true);
                    healthBar.GetComponent<HealthBar>().health = (float)health / (float)maxHealth;
                }
            }
            if (health <= 0) {
                this.gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
                if (healthBar != null) {
                    healthBar.SetActive(false);
                }
            }
        }
    }
}
