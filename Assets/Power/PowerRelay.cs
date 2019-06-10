using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerRelay : MonoBehaviour {

    public bool hasPower = true;
    public bool living = true;

    public List<GameObject> sources;
    public List<GameObject> consumer;

    public GameObject powerZone;

    public GameObject powerLines;
    public GameObject lineInstance;
    
	// Use this for initialization
	void Start () {
        powerLines = new GameObject();
        powerLines.transform.position = transform.position + 0f * Vector3.up;
        powerLines.transform.parent = transform;
        foreach (GameObject consumers in consumer) {
            //Debug.Log("MadeLine");
            GameObject line = Instantiate<GameObject>(lineInstance, powerLines.transform, false);
            line.transform.LookAt(consumers.transform.position + 0f * Vector3.up);
            //ParticleSystem ps = GetComponent<ParticleSystem>();
            //var main = ps.main;
            //main.startLifetime = Vector3.Distance(consumers.transform.position, transform.position);
        }

    }

    // Update is called once per frame
    void Update() {
        if (living && hasPower) {
            if (!powerLines.activeInHierarchy) {
                powerLines.SetActive(true);
            }
        }
        else {
            if (powerLines.activeInHierarchy) {
                powerLines.SetActive(false);
            }
        }
    }

    public void addConsumer(GameObject other) {

    }

    public void removeConsumer(GameObject other) {

    }

    public void addSource(GameObject other) {
        sources.Add(other);
        if(sources.Count == 1) {
            hasPower = true;
            foreach (GameObject consumers in consumer) {
                consumers.SendMessage("addSource", this.gameObject, SendMessageOptions.DontRequireReceiver);
            }
            powerZone.SendMessage("TurnOn");
        }
    }

    public void removeSource(GameObject other) {
        sources.Remove(other);
        if (sources.Count == 0) {
            hasPower = false;
            foreach (GameObject consumers in consumer) {
                if (consumers != null) {
                    consumers.SendMessage("removeSource", this.gameObject, SendMessageOptions.DontRequireReceiver);
                }
            }
            powerZone.SendMessage("TurnOff");
        }
    }

    public void OnDeath() {
        foreach (GameObject consumers in consumer) {
            if (consumers != null) {
                consumers.SendMessage("removeSource", this.gameObject, SendMessageOptions.DontRequireReceiver);
            }
        }
        powerZone.SendMessage("TurnOff");
    }

    public void OnRepair() {
        this.gameObject.GetComponent<BuildingHealth>().health = this.gameObject.GetComponent<BuildingHealth>().maxHealth;
        foreach (GameObject consumers in consumer) {
            consumers.SendMessage("addSource", this.gameObject, SendMessageOptions.DontRequireReceiver);
        }
        powerZone.SendMessage("TurnOn");
    }
}
