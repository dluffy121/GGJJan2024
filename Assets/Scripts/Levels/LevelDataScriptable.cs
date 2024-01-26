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
}
