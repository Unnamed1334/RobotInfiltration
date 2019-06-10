using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnLoss : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void addSource(GameObject other) {
        this.gameObject.SetActive(true);
    }

    public void removeSource(GameObject other) {
        this.gameObject.SetActive(false);
    }
}
