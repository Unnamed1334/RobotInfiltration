using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public float health;

    public GameObject green;
    public GameObject red;

    private Vector3 direction;

    // Use this for initialization
    void Start () {
        direction = new Vector3(1, 0, 1).normalized;
    }
	
	// Update is called once per frame
	void Update () {
        red.transform.localPosition = direction * (.5f - (1f - health) / 2);
        red.transform.localScale = new Vector3(.3f, .3f, 1f - health);

        green.transform.localPosition = direction * (-.5f + health / 2);
        green.transform.localScale = new Vector3(.3f, .3f, health);
    }
}
