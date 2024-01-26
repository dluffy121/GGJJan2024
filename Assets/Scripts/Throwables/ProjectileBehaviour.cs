using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField]
    float m_projectileSpeed;
    [SerializeField]
    float m_rotationSpeed;
    [SerializeField]
    Vector3 m_throwDirection;
    [SerializeField]
    int m_projectileScore;


    private float m_randomProjectile;

    public float MaxAngle { get; set; }
    public float MinAngle { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        m_randomProjectile = Random.Range(MinAngle, MaxAngle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(m_randomProjectile, m_throwDirection.y, 0) * m_projectileSpeed * Time.deltaTime;
        Vector3 l_rotation = transform.localEulerAngles;
        l_rotation.z += Time.deltaTime * m_rotationSpeed;
        transform.localEulerAngles = l_rotation;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.transform.CompareTag("Platform"))
        {
            GameEvents.OnDropProjectile?.Invoke();
            Destroy(this.gameObject);
        }
        else if (collider.transform.CompareTag("Thela"))
        {
            GameEvents.updateScore?.Invoke(m_projectileScore);
            Destroy(this.gameObject);
        }
    }
}
