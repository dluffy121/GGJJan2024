using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour 
{
    static GameEvents s_instance = null;
    public static GameEvents Instance => s_instance;


    public delegate void UpdateScore(int a_scoreAdd);
    public static UpdateScore updateScore;

    public delegate void ScoreUpdated(int a_Score);
    public static ScoreUpdated OnScoreUpdated;
    public static ScoreUpdated OnRequiredLevelScoreChanged;

    public delegate void DropProjectile();
    public static DropProjectile OnDropProjectile;

    public delegate void LiveLost(int a_LivesLeft);
    public static LiveLost OnLiveLost;
    public static LiveLost OnTotalLivesUpdated;

    public delegate void PlayerStun();
    public static PlayerStun OnPlayerStun;

    public delegate void UpdateSpawners();
    public static UpdateSpawners updateSpawners;

    private void Awake()
    {
        s_instance = this;
    }

    private void OnDestroy()
    {
        s_instance = null;
    }

}
