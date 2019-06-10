using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public float difficulty;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        GameObject.Find("Difficulty").GetComponent<Difficulty>().difficulty = this.difficulty;
        SceneManager.LoadScene(1);
    }

}
