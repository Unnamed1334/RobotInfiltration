using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour {

    public float speed = 20;
    public float timeout = 1;
    public float radius = .25f;

    public int damage = 10;

    public LayerMask lm;

	// Use this for initialization
	void Start () {
		
	} 
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, lm);
        if (hit.Length > 0) {
            foreach(Collider hitObject in hit) {
                hitObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
            Destroy(this.gameObject);
        }
        timeout -= Time.deltaTime;
        if(timeout <= 0) {
            Destroy(this.gameObject);
        }
	}
}
