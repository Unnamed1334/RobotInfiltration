using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public LayerMask mask;

    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void LateUpdate() {
        if (Input.GetMouseButton(1)) {
            Ray mouseDirection = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(mouseDirection, out hit, 200f, mask);
            transform.position = (hit.point + player.transform.position) / 2 + new Vector3(20, 30, -20);
        }
        else {
            transform.position = (player.transform.position) + new Vector3(20, 30, -20);
        }
    }
}
