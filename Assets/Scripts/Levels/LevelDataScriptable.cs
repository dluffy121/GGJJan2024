using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataScriptableObject", menuName = "ScriptableObject/LevelData")]
public class LevelDataScriptable : ScriptableObject
{
    public RequiredDataForLevel[] levelsData;
}

[System.Serializable]
public class RequiredDataForLevel
{
    public int numberOfLives;
    public int requiredScoreToWin;
    public int[] listOfSpawners;
    [SerializeField]
    private int m_probabilitySmallItems;
    [SerializeField]
    private int m_probabilityMediumItems;
    [SerializeField]
    private int m_probabilityLargeItems;
    [SerializeField]
    private int m_probabilityObstacles;

    public int ProbabSmallItem { get { return m_probabilitySmallItems; } }
    public int ProbabMediumItem { get { return m_probabilityMediumItems; } }
    public int ProbabLargeItem { get { return m_probabilityLargeItems; } }
    public int ProbabilityObstacles {  get { return m_probabilityObstacles; } }
}
