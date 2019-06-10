using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {

    public LayerMask mask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray mouseDirection = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(mouseDirection, out hit, 200f, mask);
        Vector3 target = hit.point;
        target.y = transform.position.y;
        transform.LookAt(target, Vector3.up);
	}
}
