using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Used for mobile units
*/
public class PowerConsumer : MonoBehaviour {

    public bool hasPower = false;
    public bool hasPowerSource = false;

    public float maxPower = 20f;
    public float power = 20f;

    public List<GameObject> sources;
    public GameObject powerIcon;
    public PowerBar powerScript;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!hasPowerSource && power > 0) {
            power -= Time.deltaTime;
        }
        if (hasPowerSource && power < maxPower) {
            power += 3 * Time.deltaTime;
        }

        if (power < 0) {
            power = 0;
            hasPower = false;
        }
        if( power > maxPower) {
            power = maxPower;
            powerIcon.SetActive(false);
        }

        if(power < maxPower) {
            powerScript.power = power / maxPower;
        }
    }


    public void addSource(GameObject other) {
        sources.Add(other);
        if (sources.Count == 1) {
            hasPowerSource = true;
            hasPower = true;
        }
    }

    public void removeSource(GameObject other) {
        sources.Remove(other);
        if (sources.Count == 0) {
            hasPowerSource = false;
            powerIcon.SetActive(true);
        }
    }

}
