using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator m_anim;
    [SerializeField]
    private Rigidbody2D m_Rigidbody;
    [SerializeField]
    private float m_playerSpeed;
    [Tooltip("Add The backward Speed to be decreased")]
    [SerializeField]
    private float m_backSpeedMultiplier;
    [SerializeField]
    private KeyCode m_rightKeyCode;
    [SerializeField]
    private KeyCode m_leftKeyCode;
    [SerializeField]
    private bool m_isLeftCharacter;

    void Update()
    {
        if (Input.GetKey(m_rightKeyCode))
        {
            m_anim.Play("Walk");
            if(m_isLeftCharacter)
                m_Rigidbody.AddForce(Vector2.right * m_playerSpeed * Time.deltaTime);
            else
                m_Rigidbody.AddForce(Vector2.right * m_playerSpeed * m_backSpeedMultiplier * Time.deltaTime);
        }
        else if (Input.GetKey(m_leftKeyCode))
        {
            m_anim.Play("WalkBack");
            if (m_isLeftCharacter)
                m_Rigidbody.AddForce(Vector2.left * m_playerSpeed * m_backSpeedMultiplier * Time.deltaTime);
            else
                m_Rigidbody.AddForce(Vector2.left * m_playerSpeed * Time.deltaTime);
        }
        else
            m_anim.Play("Idle");
    }
}
