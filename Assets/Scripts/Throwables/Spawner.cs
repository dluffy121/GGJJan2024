using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    float m_spawnTime;
    [SerializeField]
    GameObject[] m_GoProjectiles;
    [SerializeField]
    float m_minAngle;
    [SerializeField]
    float m_maxAngle;

    private float m_timeLeftToSpawn;

    private void Start()
    {
        m_timeLeftToSpawn = m_spawnTime;
    }
    // Update is called once per frame
    void Update()
    {
        m_timeLeftToSpawn -= Time.deltaTime;
        if (m_timeLeftToSpawn < 0)
        {
            GameObject l_Projectile = Instantiate(m_GoProjectiles[Random.Range(0, m_GoProjectiles.Length)],gameObject.transform);
            ProjectileBehaviour projectileBehaviour = l_Projectile.GetComponent<ProjectileBehaviour>();
            if (projectileBehaviour != null)
            {
                projectileBehaviour.MaxAngle = m_maxAngle;
                projectileBehaviour.MinAngle = m_minAngle;
            }
            m_timeLeftToSpawn = m_spawnTime;
        }
    }
}
