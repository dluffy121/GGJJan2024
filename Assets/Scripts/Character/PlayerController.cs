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
    [SerializeField]
    private float m_playerStunTime;
    [SerializeField]
    private GameObject m_playerStun;
    [SerializeField]
    private Animator m_StunAnim;
    [SerializeField]
    private Transform m_light;

    private bool m_isPlayerStun;

    private void Start()
    {
        GameEvents.OnPlayerStun += CallbackOnPlayerStun;
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerStun -= CallbackOnPlayerStun;
    }

    private void CallbackOnPlayerStun(string a_name)
    {
        if (a_name.Equals(gameObject.name))
        {
            m_isPlayerStun = true;
            m_playerStun.SetActive(true);
            m_StunAnim.Play("Stun");
            StartCoroutine(IEWaitToReActivatePlayerControls());
        }
    }

    IEnumerator IEWaitToReActivatePlayerControls()
    {
        yield return new WaitForSeconds(m_playerStunTime);
        m_playerStun.SetActive(false);
        m_isPlayerStun = false;
    }

    void Update()
    {
        m_light.position = new Vector3(m_Rigidbody.gameObject.transform.position.x, m_light.position.y, m_light.position.z);
        if (!m_isPlayerStun)
        {
            if (Input.GetKey(m_rightKeyCode))
            {
                m_anim.Play("Walk");
                if (m_isLeftCharacter)
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
}
