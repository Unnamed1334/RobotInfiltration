using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour {

    public float power;

    public GameObject black;
    public GameObject green;
    public GameObject red;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        black.transform.localPosition = new Vector3(0, .3f + -.3f * (1f - power), 0);
        black.transform.localScale = new Vector3(.5f, .6f * (1f - power), .5f);

        if (power >= .25f) {
            green.transform.localPosition = new Vector3(0, -.3f + .3f * power, 0);
            green.transform.localScale = new Vector3(.5f, .6f * power, .5f);

            red.transform.localPosition = new Vector3(0, -.3f, 0);
            red.transform.localScale = new Vector3(.5f, .0f, .5f);
        }
        else {
            red.transform.localPosition = new Vector3(0, -.3f + .3f * power, 0);
            red.transform.localScale = new Vector3(.5f, .6f * power, .5f);

            green.transform.localPosition = new Vector3(0, -.3f, 0);
            green.transform.localScale = new Vector3(.5f, .0f, .5f);
        }
    }
}
