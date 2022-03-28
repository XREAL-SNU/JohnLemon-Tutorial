using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
   public NavMeshAgent NavMeshAgent;
   public Transform[] waypoints;
   int m_CurrentWaypointIndex;

    void Start()
    {
        NavMeshAgent.SetDestination(waypoints[0].position);
    }

    
    void Update()
    {
        if (NavMeshAgent.remainingDistance < NavMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            NavMeshAgent.SetDestination (waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
