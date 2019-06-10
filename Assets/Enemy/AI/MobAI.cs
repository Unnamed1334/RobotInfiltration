using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobAI : MonoBehaviour {

    public int state;

    public float sightRange;
    public LayerMask sightMask;
    public float notifyRange;

    public GameObject projectile;
    public Vector3 attackOffset;
    public float attackRange;
    public int attackDamage;
    //public float attackWindupMax;
    //public float attackWindup;
    public float attackCooldownMax;
    public float attackCooldown;


    public GameObject nextNode;
    public GameObject lastSource;
    public GameObject player;

    public NavMeshAgent agent;
    public PowerConsumer power;

    private int navLimit = 30;
    public int lastState;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        power = GetComponent<PowerConsumer>();

        //default behavior is to do nothing
        if (nextNode == null) {
            nextNode = new GameObject();
            nextNode.transform.position = transform.position;
            nextNode.AddComponent<Node>();
        }

        float difficulty = GameObject.Find("Difficulty").GetComponent<Difficulty>().difficulty;
        if (difficulty != 0) {
            attackCooldownMax /= difficulty;
            if (agent.speed != 0) {
                agent.speed = agent.speed - 2 + difficulty * 2;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (power.hasPower) {
            if (navLimit > 0) {
                navLimit--;
            }
            switch (state) {
                case 0: //to next node
                    if (PlayerVisable()) {
                        state = 2;
                        NotifyAllies();
                    }
                    if (!agent.hasPath) {
                        agent.SetDestination(nextNode.transform.position);
                    }
                    float x = transform.position.x - nextNode.transform.position.x;
                    float z = transform.position.z - nextNode.transform.position.z;
                    if (x * x + z * z < 1) {
                        if (nextNode.GetComponent<Node>().next != null) {
                            nextNode = nextNode.GetComponent<Node>().next;
                            agent.SetDestination(nextNode.transform.position);
                        }
                        else {
                            state = 1;
                        }
                    }
                    break;
                case 1: //holding position
                    if (PlayerVisable()) {
                        state = 2;
                        NotifyAllies();
                    }
                    break;
                case 2: //Chasing player
                    if (navLimit <= 0) {
                        if (PlayerVisable()) {
                            agent.SetDestination(player.transform.position);
                        }
                    }
                    if(PlayerVisable() && Vector3.Distance(player.transform.position, transform.position) < attackRange) {
                        agent.ResetPath();
                        lastState = state;
                        state = 6;
                        attackCooldown = attackCooldownMax;
                        if (projectile == null) {
                            player.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
                        }
                        else {
                            Vector3 target = player.transform.position;
                            target.y = transform.position.y;
                            transform.LookAt(target, Vector3.up);
                            Instantiate(projectile, transform.position + attackOffset, transform.rotation);
                        }
                    }
                    else if (lastSource != null && power.power < power.maxPower / 2) {
                        agent.SetDestination(lastSource.transform.position);
                        state = 3;
                    }
                    break;
                case 3: //Fleeing
                    if (power.power > .9f * power.maxPower) {
                        state = 0;
                    }
                    break;
                case 4: //Recharging
                    if (power.power > .9f * power.maxPower) {
                        state = 0;
                    }
                    if (PlayerVisable() && Vector3.Distance(player.transform.position, transform.position) < attackRange) {
                        agent.ResetPath();
                        lastState = state;
                        state = 6;
                        attackCooldown = attackCooldownMax;
                        if (projectile == null) {
                            player.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
                        }
                        else {
                            Vector3 target = player.transform.position;
                            target.y = transform.position.y;
                            transform.LookAt(target, Vector3.up);
                            Instantiate(projectile, transform.position + attackOffset, transform.rotation);
                        }
                    }
                    if (lastSource != null && !power.hasPowerSource) {
                        agent.SetDestination(lastSource.transform.position);
                        state = 3;
                    }
                    break;
                case 5: //enraged
                    if (navLimit <= 0) {
                        if (PlayerVisable()) {
                            agent.SetDestination(player.transform.position);
                        }
                    }
                    if (PlayerVisable() && Vector3.Distance(player.transform.position, transform.position) < attackRange) {
                        agent.ResetPath();
                        lastState = state;
                        state = 6;
                        attackCooldown = attackCooldownMax;
                        if (projectile == null) {
                            player.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
                        }
                        else {
                            Vector3 target = player.transform.position;
                            target.y = transform.position.y;
                            transform.LookAt(target, Vector3.up);
                            Instantiate(projectile, transform.position + attackOffset, transform.rotation);
                        }
                    }
                    break;
                case 6: //attack cooldown
                    attackCooldown -= Time.deltaTime;
                    if (attackCooldown < 0) {
                        state = lastState;
                    }
                    break;
                default: //what
                    state = 0;
                    break;
            }
        }
        else {
            if (state != -1) {
                state = -1;
                agent.ResetPath();
            }
        }
	}


    public void  sourceDestroyed () {
        if (state == 6) {
            lastState = 5;
        }
        else {
            state = 5;
        }
    }

    public void addSource(GameObject other) {
        lastSource = other;
    }


    private bool PlayerVisable() {
        RaycastHit hit;
        //Debug.DrawRay(transform.position, player.transform.position - transform.position);
        if (Physics.Raycast(new Ray(transform.position + Vector3.up * .5f, player.transform.position - transform.position), out hit, sightRange, sightMask)) {
            //Debug.Log(hit.collider.gameObject.name);
            return (hit.collider.gameObject.Equals(player));//did we hit the player
        }
        else {
            return false;
        }
    }

    public void TakeDamage(int damage) {
        if (state == 0 || state == 1) {
            agent.SetDestination(player.transform.position);
        }
    }

    public void OnNotify() {
        state = 2;
        agent.SetDestination(player.transform.position);
    }

    private void NotifyAllies() {
        //Debug.DrawRay(transform.position, player.transform.position - transform.position);
        Collider[] hit = Physics.OverlapSphere(transform.position, notifyRange);
        for(int i = 0; i < hit.Length; i++) {
            hit[i].gameObject.SendMessage("OnNotify", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void OnDeath() {
       gameObject.SetActive(false);
    }

    public void OnRepair() {
        gameObject.SetActive(true);

        gameObject.GetComponent<BuildingHealth>().health = gameObject.GetComponent<BuildingHealth>().maxHealth;

        state = 0;

        agent.SetDestination(nextNode.transform.position);
    }
}
