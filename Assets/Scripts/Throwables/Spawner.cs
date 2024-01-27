using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    float m_spawnTime;
    [SerializeField]
    ProjectileTypeToSpawn[] m_arrProjectileToSpawn;
    [SerializeField]
    float m_minAngle;
    [SerializeField]
    float m_maxAngle;

    private int m_probabilitySmallItems;
    private int m_probabilityMediumItems;
    private int m_probabilityLargeItems;
    private int m_probabilityObstacles;
    private float m_timeLeftToSpawn;

    List<ProjectileBehaviour> spawnedProjectileBehaviours = new List<ProjectileBehaviour>();

    private void OnEnable()
    {
        m_timeLeftToSpawn = m_spawnTime;
        GameEvents.updateSpawners += CallbackToUpdateSpawners;
    }

    private void OnDisable()
    {
        GameEvents.updateSpawners -= CallbackToUpdateSpawners;
    }

    // Update is called once per frame
    void Update()
    {
        m_timeLeftToSpawn -= Time.deltaTime;
        if (m_timeLeftToSpawn < 0)
        {
            GameObject l_Projectile = selectProjectileToSpawn();
            ProjectileBehaviour projectileBehaviour = l_Projectile.GetComponent<ProjectileBehaviour>();
            if (projectileBehaviour != null)
            {
                projectileBehaviour.MaxAngle = m_maxAngle;
                projectileBehaviour.MinAngle = m_minAngle;
                spawnedProjectileBehaviours.Add(projectileBehaviour);
            }
            m_timeLeftToSpawn = m_spawnTime;
        }
    }

    private void CallbackToUpdateSpawners()
    {
        int l_currentLevel = LevelManager.LevelToLoad;
        RequiredDataForLevel l_currentLevelData = LevelManager.DataForLevels.levelsData[l_currentLevel];
        m_probabilitySmallItems = l_currentLevelData.ProbabSmallItem;
        m_probabilityMediumItems = l_currentLevelData.ProbabMediumItem;
        m_probabilityLargeItems = l_currentLevelData.ProbabLargeItem;
        m_probabilityObstacles = l_currentLevelData.ProbabilityObstacles;
    }

    public void ClearTheProjectilesInLevel()
    {
        for(int i = 0; i < spawnedProjectileBehaviours.Count; i++)
        {
            if(spawnedProjectileBehaviours[i] != null)
            {
                Destroy(spawnedProjectileBehaviours[i].gameObject);
            }
        }

        spawnedProjectileBehaviours.Clear();
    }

    private GameObject selectProjectileToSpawn()
    {
        int l_selectedType = Random.Range(0, 100);
        EProjectileType l_selectedProjectileType = EProjectileType.None;
        if (l_selectedType <= m_probabilitySmallItems)
            l_selectedProjectileType = EProjectileType.SmallProjectile;
        else if (l_selectedType > m_probabilitySmallItems && l_selectedType <= m_probabilitySmallItems + m_probabilityMediumItems)
            l_selectedProjectileType = EProjectileType.MediumProjectile;
        else if (l_selectedType > m_probabilitySmallItems + m_probabilityMediumItems && l_selectedType <= m_probabilitySmallItems + m_probabilityMediumItems + m_probabilityLargeItems)
            l_selectedProjectileType = EProjectileType.LargeProjectile;
        else if(l_selectedType > m_probabilitySmallItems + m_probabilityMediumItems + m_probabilityLargeItems && l_selectedType <= 100)
            l_selectedProjectileType = EProjectileType.Obstacle;
        ProjectileTypeToSpawn l_selectedProjectileTypeToSpawn = System.Array.Find(m_arrProjectileToSpawn,l_ProjectileToSpawn => l_ProjectileToSpawn.ProjectileType.Equals(l_selectedProjectileType));
        return Instantiate(l_selectedProjectileTypeToSpawn.ArrGOProjectile[Random.Range(0, l_selectedProjectileTypeToSpawn.ArrGOProjectile.Length)], gameObject.transform);
    }
}

public enum EProjectileType : byte
{
    None = 0,
    SmallProjectile = 1,
    MediumProjectile = 2,
    LargeProjectile = 3,
    Obstacle = 4
}

[System.Serializable]
public class ProjectileTypeToSpawn
{
    [SerializeField]
    private EProjectileType m_ProjectileType;
    [SerializeField]
    private GameObject[] m_arrGOProjectile;

    public EProjectileType ProjectileType { get { return m_ProjectileType; } }
    public GameObject[] ArrGOProjectile { get { return m_arrGOProjectile; } }
}