using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour {
    public Transform player;
    public GameEnding gameEnding;
    public const float aggroGain = 1.5f;
    public const float aggroWear = 0.1f;

    public float callCooldown = 0f;
    public float aggro = 0f;
    public bool aggroing = false;

    bool m_IsPlayerInRange;
    public bool playerInSight;

    public AudioSource scream;

    void OnTriggerEnter(Collider other) {
        if (other.transform == player) {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.transform == player) {
            m_IsPlayerInRange = false;
        }
    }

    private void Start() {
        aggro = 0f;
        aggroing = false;
    }

    void Update() {
        playerInSight = false;
        if (m_IsPlayerInRange) {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit)) {
                if (raycastHit.collider.transform == player) {
                    float r = Mathf.Clamp01(1f - raycastHit.distance / 5f) + 0.01f;
                    //Debug.Log(r);
                    aggro += Time.deltaTime * aggroGain * r;
                    if (aggro >= 1f) {
                        if(!aggroing) scream.Play();
                        aggroing = true;
                        AggroStart();
                        aggro = 1f;
                    }

                    playerInSight = true;
                }
            }
        }
        else {
            if (aggroing) {
                Vector3 direction = player.position - transform.position + Vector3.up;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit raycastHit;
                playerInSight = Physics.Raycast(ray, out raycastHit) && raycastHit.collider.transform == player;
            }

            if (aggro > 0f) aggro -= Time.deltaTime * aggroWear * (playerInSight ? 0.1f : 1f);
            if (aggro <= 0f && aggroing) aggroing = false;
        }

        if (callCooldown > 0f) {
            callCooldown -= Time.deltaTime;
        }
    }

    public void AggroStart() {
        if (callCooldown > 0f) return;
        callCooldown = 5f;
        WaypointPatrol.EachPatrol(p => p.target = player.transform.position);

        WaypointPatrol p = transform.parent.GetComponent<WaypointPatrol>();
        if (p != null) p.AggroFull();
    }
}