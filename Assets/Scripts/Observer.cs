using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{

    public Transform player;

    public GameEnding gameEnding;
    bool m_IsPlayerInRange;

    


    private void OnTriggerEnter(Collider other)
    //트리거에서는 조건 만들어주고, 그 조건인 bool을 실행조건으로 하는 함수를 만든다.
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
            //Debug.Log("Player찾았당");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);

            //The Raycast method you will be using returns a bool
            //which is true when it has hit something and false when it hasn’t hit anything. 

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))//bool말고 뭐 부딪쳤는지 알려줌
            {
                //Debug.Log("Raycast");
                //Debug.DrawRay(transform.position, ray.direction * 10f, Color.red, 5f);

                if (raycastHit.collider.transform == player)
                {
                    //Debug.Log("감지");
                    gameEnding.CaughtPlayer();
                }
            }

        }
    }
}
