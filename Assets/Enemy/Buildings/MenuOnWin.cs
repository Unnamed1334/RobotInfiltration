using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOnWin : MonoBehaviour {

    public bool win = false;
    public float timer = 3;

    public GameObject vicoryObject;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (win) {
            timer -= Time.deltaTime;

            if (timer <= 0) {
                SceneManager.LoadScene(0);
            }

        }
    }


    public void OnDeath() {
        win = true;
        Instantiate(vicoryObject, Camera.main.transform, false);
    }
}