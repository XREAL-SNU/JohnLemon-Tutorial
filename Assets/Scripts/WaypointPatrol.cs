using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour {
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    public const float speed = 0.9f, aggroSpeed = 1.45f;

    //public static Vector3? globalTarget = null;
    public Vector3? target = null;
    //public float aggro = 0f;
    //public bool chasingPlayer = false;

    public GameObject player;
    public Observer observer;

    int m_CurrentWaypointIndex;

    void Start() {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update() {
        if (observer.aggroing) {
            target = player.transform.position;
            navMeshAgent.speed = aggroSpeed;
        }
        else navMeshAgent.speed = speed;

        //aggro
        if(target != null) {
            if (target != navMeshAgent.destination) navMeshAgent.SetDestination((Vector3)target);
            if (navMeshAgent.remainingDistance < 0.1f) {
                target = null;
            }
        }
        //patrol
        else if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance) {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    public void AggroFull() {
        //todo shout
    }

    public static void EachPatrol(Action<WaypointPatrol> act) {
        GameObject[] patrols = GameObject.FindGameObjectsWithTag("Patrol");
        foreach(GameObject o in patrols) {
            act(o.GetComponent<WaypointPatrol>());
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            observer.gameEnding.CaughtPlayer();
        }
    }
}
