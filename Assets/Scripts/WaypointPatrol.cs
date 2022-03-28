using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour {
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public Transform[] waypoints;

    public const float speed = 1.2f, aggroSpeed = 2f;

    //public static Vector3? globalTarget = null;
    public Vector3? target = null;
    //public float aggro = 0f;
    //public bool chasingPlayer = false;

    public GameObject player;
    public Observer observer;

    int m_CurrentWaypointIndex;

    void Start() {
        animator = GetComponent<Animator>();
        animator.ResetTrigger("shout");
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update() {
        if (observer.aggroing) {
            if(observer.playerInSight)target = player.transform.position;
            navMeshAgent.speed = aggroSpeed;
        }
        else {
            navMeshAgent.speed = speed;
            animator.ResetTrigger("shout");
        }

        //aggro
        if(target != null) {
            if (target != navMeshAgent.destination) navMeshAgent.SetDestination((Vector3)target);
            else if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance / 2f) {
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
        //todo shout sound
        animator.SetTrigger("shout");
        //Debug.Log("shout");
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
