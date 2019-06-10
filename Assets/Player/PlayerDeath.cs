using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {

    public bool dead = false;
    public float timeout = 3f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (dead) {
            timeout -= Time.deltaTime;
            if (timeout <= 0) {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void OnDeath() {
        dead = true;
        //this.gameObject.SetActive(false);
    }
}
