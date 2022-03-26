using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    public float turnSpeed =20f;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator=GetComponent<Animator>();
        m_Rigidbody=GetComponent<Rigidbody>();
        m_AudioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical=Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput= !Mathf.Approximately(horizontal, 0f); //horizontal 이 0과 근접하면 true반환
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        if(isWalking)
        {
            if(!m_AudioSource.isPlaying)
                m_AudioSource.Play();
        }
        else
        {
            m_AudioSource.Stop();
        }
        //가는 방향으로 회전하기
        Vector3 desiredForward=Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed *Time.deltaTime, 0f);
        m_Rotation=Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        //GameObject self를 옮기는 게 아니라 rigidbody를 옮기는 이유?
        //해당 코드 잘 이해가 안 감
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
