using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private static int m_setScore = 0;
    [SerializeField]
    int m_levelScore = 10000;
    [SerializeField]
    int m_livesLeft;

    static LevelManager s_instance = null;
    public static LevelManager Instance => s_instance;

    private void Awake()
    {
        s_instance = this;
    }

    private void OnDestroy()
    {
        GameEvents.updateScore -= CallbackToUpdateScore;
        GameEvents.OnDropProjectile -= CallbackOnDropProjectile;
        s_instance = null;
    }

    private void Start()
    {
        GameEvents.updateScore += CallbackToUpdateScore;
        GameEvents.OnDropProjectile += CallbackOnDropProjectile;
    }

    private void CallbackToUpdateScore(int a_addScore)
    {
        m_setScore += a_addScore;
        if (m_setScore >= m_levelScore)
            Debug.Log("!! Level Completed !!");
        GameEvents.OnScoreUpdated?.Invoke(m_setScore);
    }

    private void CallbackOnDropProjectile()
    {
        m_livesLeft--;
        if (m_livesLeft <= 0)
            Debug.Log(":( Level Failed");
        GameEvents.OnLiveLost?.Invoke(m_livesLeft);
    }
}
