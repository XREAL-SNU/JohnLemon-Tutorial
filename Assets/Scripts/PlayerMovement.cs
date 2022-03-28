using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float turnSpeed = 20f, walkSpeed = 0.7f, runSpeed = 1.2f;

    public Vector3 movement;
    public Quaternion rotation;
    public bool walking;

    Animator animator;
    Rigidbody rigid;
    AudioSource m_AudioSource;
    void Start() {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        walking = false;
    }

    void FixedUpdate() { //always called before OnAnimatorMove, unlike Update
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");
        bool running = Input.GetKey(KeyCode.LeftShift);

        movement.Set(hori, 0, verti);
        float mlen = movement.magnitude;

        if (mlen > 1f) {
            movement.Normalize();
            mlen = 1f;
        }

        walking = !Mathf.Approximately(mlen, 0f);
        animator.SetBool("isWalking", walking);
        movement *= running ? runSpeed : walkSpeed;
        animator.SetFloat("walkSpeed", (running ? runSpeed : walkSpeed) * mlen);

        //do the moving
        Vector3 df = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(df);

        if (walking) {
            if (!m_AudioSource.isPlaying) {
                m_AudioSource.Play();
            }
            m_AudioSource.pitch = running ? runSpeed : walkSpeed;
        }
        else {
            m_AudioSource.Stop();
        }
    }

    void OnAnimatorMove() {
        rigid.MovePosition(rigid.position + movement * animator.deltaPosition.magnitude);
        rigid.MoveRotation(rotation);
    }
}
