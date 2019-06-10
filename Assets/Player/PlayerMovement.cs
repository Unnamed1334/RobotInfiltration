using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    public NavMeshAgent agent;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();

    }
 
    // Update is called once per frame
    void Update() { 
        if(Input.GetKeyDown(KeyCode.B)) {
            agent.speed *= 1.1f;
        }
        Vector3 movementVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            movementVector += new Vector3(-1, 0, 1);
        }
        if (Input.GetKey(KeyCode.S)) {
            movementVector += new Vector3(1, 0, -1);
        }
        if (Input.GetKey(KeyCode.A)) {
            movementVector += new Vector3(-1, 0, -1);
        }
        if (Input.GetKey(KeyCode.D)) {
            movementVector += new Vector3(1, 0, 1);
        }
        agent.SetDestination(transform.position + movementVector.normalized * agent.speed * Time.deltaTime);
        agent.Move(movementVector.normalized * agent.speed * Time.deltaTime);
        Debug.DrawLine(transform.position + movementVector.normalized * 10* agent.speed * Time.deltaTime - Vector3.up, transform.position + movementVector.normalized * 10 * agent.speed * Time.deltaTime + Vector3.up);
    }
}
