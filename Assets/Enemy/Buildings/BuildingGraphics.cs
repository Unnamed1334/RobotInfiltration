using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGraphics : MonoBehaviour {

    public GameObject currentArt;
    public GameObject workingArt;
    public GameObject destroyedArt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDeath() {
        Destroy(currentArt);
        currentArt = Instantiate<GameObject>(destroyedArt, this.transform, false);
    }

    public void OnRepair() {
        Destroy(currentArt);
        currentArt = Instantiate<GameObject>(workingArt, this.transform, false);
    }
}
