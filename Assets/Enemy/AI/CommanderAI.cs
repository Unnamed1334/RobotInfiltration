using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderAI : MonoBehaviour {

    public float difficulty;

    public GameObject factory;
    public GameObject[] repair;

    public Queue<GameObject> buildList;
    public Queue<GameObject> repairList;

    public float cooldown;
    public float maxCooldown = 15;

    public float repairCooldown;
    public float maxRepairCooldown = 30;

    // Use this for initialization
    void Start () {
        buildList = new Queue<GameObject>();
        repairList = new Queue<GameObject>();

        difficulty = GameObject.Find("Difficulty").GetComponent<Difficulty>().difficulty;
        if(difficulty != 0) {
            if (difficulty < 4) {
                cooldown /= difficulty;
                maxCooldown /= difficulty;
                repairCooldown /= difficulty;
                maxRepairCooldown /= difficulty;
            }
            else {
                cooldown /= 4;
                maxCooldown /= 4;
                repairCooldown /= 4;
                maxRepairCooldown /= 4;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (buildList.Count > 0 && factory.activeInHierarchy) {
            if (cooldown > 0) {
                cooldown -= Time.deltaTime;
            }
            if (cooldown <= 0) {
                cooldown = maxCooldown;
                GameObject newEnemy = buildList.Dequeue();
                newEnemy.transform.position = factory.transform.position;
                newEnemy.SetActive(true);
                newEnemy.SendMessage("OnRepair", SendMessageOptions.DontRequireReceiver);
            }
        }

        if (repairList.Count > 0) {
            if (repairCooldown > 0) {
                repairCooldown -= Time.deltaTime;
            }
            if (repairCooldown <= 0) {
                repairCooldown = maxRepairCooldown;
                GameObject newEnemy = repairList.Dequeue();
                newEnemy.SendMessage("OnRepair", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void RepairDone() {
        
    }

    public void QueueReplace(GameObject deadObject) {
        buildList.Enqueue(deadObject);
    }

    public void QueueRepair(GameObject deadObject) {
        repairList.Enqueue(deadObject);
    }
}
