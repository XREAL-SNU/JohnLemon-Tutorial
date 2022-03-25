using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 m_Movement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        
        
        
    }
}
