using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParrent : MonoBehaviour {

	// Use this for initialization
	void Start () {
        for (int i = this.gameObject.transform.childCount - 1; i >= 0; i--) {
            this.gameObject.transform.GetChild(i).SetParent(null);
        }
        Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
