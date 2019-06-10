using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerZone : MonoBehaviour {

    public bool powered;
    public List<GameObject> containedUnits;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TurnOn() {
        powered = true;
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        foreach (GameObject consumers in containedUnits) {
            if (consumers != null) {
                consumers.SendMessage("addSource", this.gameObject, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void TurnOff() {
        powered = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        foreach (GameObject consumers in containedUnits) {
            if (consumers != null) {
                consumers.SendMessage("removeSource", this.gameObject, SendMessageOptions.DontRequireReceiver);
                consumers.SendMessage("sourceDestroyed", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        containedUnits.Add(other.gameObject);
        if(powered) {
            other.gameObject.SendMessage("addSource", this.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }


    void OnTriggerExit(Collider other) {
        containedUnits.Remove(other.gameObject);
        if (powered) {
            other.gameObject.SendMessage("removeSource", this.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}
