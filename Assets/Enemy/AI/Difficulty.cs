using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour {

    public float difficulty = 1;

    // Use this for initialization
    void Start() {
        if (!GameObject.Find("Difficulty")) {
            gameObject.name = "Difficulty";
            Object.DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
