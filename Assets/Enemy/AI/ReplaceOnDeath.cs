using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceOnDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDeath() {
        //gameObject.SetActive(false);
        GameObject.Find("AI_Commander").GetComponent<CommanderAI>().QueueReplace(this.gameObject);
        //Destroy(this.gameObject);
    }
}
