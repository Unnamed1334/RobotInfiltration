using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour {

    public List<GameObject> consumer;

    public GameObject powerZone;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { 
		
	}

    public void OnDeath() {
        foreach (GameObject consumers in consumer) {
            consumers.SendMessage("removeSource", this.gameObject);
        }
        powerZone.SendMessage("TurnOff");
    }

    public void OnRepair() {
        foreach (GameObject consumers in consumer) {
            consumers.SendMessage("addSource", this.gameObject);
        }
        powerZone.SendMessage("TurnOn");
    }
}
