using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f; // direction of vector

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity; // save rotation information

    void Start()
    {
        m_Animator = GetComponent<Animator> (); // a reference for <Animator> type component 
        m_Rigidbody = GetComponent<Rigidbody>(); // a reference for rigidbody. 
        m_AudioSource = GetComponent<AudioSource>(); // a reference for audio source component.

    }

    void FixedUpdate() // to set up moving vector and rotation on time using `OnAnimatorMove`
    {
        float horizontal = Input.GetAxis("Horizontal"); // horizontal axis input value
        float vertical = Input.GetAxis("Vertical"); // vertical axis input value

        m_Movement.Set(horizontal, 0f, vertical); // set variables for vector
        m_Movement.Normalize();  // normalize the value 

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f); // recognize player input (horizontal axis)
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);  // recognize player input (vertical axis)
        bool isWalking = hasHorizontalInput || hasVerticalInput; //  bool variable indicating animator's action `is it walking?` 
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else 
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f); // for every second, (change) x (time consumed)
        m_Rotation = Quaternion.LookRotation(desiredForward); // rotate to desired direction 


    }

    void OnAnimatorMove() 
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude); // length of deltaPosition의 * Vector representing moving direction 
        m_Rigidbody.MoveRotation (m_Rotation);


    }
}
