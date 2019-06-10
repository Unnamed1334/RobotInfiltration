using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 20 * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.parent.rotation, 20 * Time.deltaTime);
    }
}
