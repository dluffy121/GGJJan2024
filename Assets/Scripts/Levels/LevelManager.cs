using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private static int m_setScore = 0;
    [SerializeField]
    int m_levelScore = 10000;
    [SerializeField]
    int m_livesLeft;
    [SerializeField]
    LevelDataScriptable m_dataForLevels;

    [SerializeField]
    Spawner[] m_projectilesSpawnerRefrenceList;

    [SerializeField]
    GameSceneManager m_gameSceneManagerRefrence;
    [SerializeField]
    AudioClip levelPassSound, levelFailedSound;
    int m_levelToLoad = 0;

    static LevelManager s_instance = null;
    public static LevelManager Instance => s_instance;

    public static LevelDataScriptable DataForLevels { get { return Instance.m_dataForLevels; } }
    public static int LevelToLoad {  get { return Instance.m_levelToLoad; } }

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
        LoadLevel(m_levelToLoad);
        GameEvents.updateScore += CallbackToUpdateScore;
        GameEvents.OnDropProjectile += CallbackOnDropProjectile;
    }

    private void CallbackToUpdateScore(int a_addScore)
    {
        m_setScore += a_addScore;
        if (m_setScore >= m_levelScore)
        {
            Debug.Log("!! Level Completed !!");
            SoundManager.PlaySoundEffect(levelPassSound);
            GamePlayUICanvas.SetLevelCompletePanelVisibility(true);
            GameManager.Instance.PauseTheGame(true);
        }
        GameEvents.OnScoreUpdated?.Invoke(m_setScore);
    }

    private void CallbackOnDropProjectile()
    {
        m_livesLeft--;
        if (m_livesLeft <= 0)
        {
            Debug.Log(":( Level Failed");
            SoundManager.PlaySoundEffect(levelFailedSound);
            GamePlayUICanvas.SetLevelFailedPanelVisibility(true);
            GameManager.Instance.PauseTheGame(true);
        }
        GameEvents.OnLiveLost?.Invoke(m_livesLeft);
    }

    private void LoadLevel(int a_levelNo)
    {
        for (int i = 0; i < m_projectilesSpawnerRefrenceList.Length; i++)
        {
            m_projectilesSpawnerRefrenceList[i].ClearTheProjectilesInLevel();
            m_projectilesSpawnerRefrenceList[i].gameObject.SetActive(false);
        }

        int l_iterations = m_dataForLevels.levelsData[a_levelNo].listOfSpawners.Length;
        for(int i = 0; i < l_iterations; i++)
        {
            m_projectilesSpawnerRefrenceList
                [m_dataForLevels.levelsData[a_levelNo].listOfSpawners[i]].gameObject.SetActive(true);
        }
        m_setScore = 0;
        m_levelScore = m_dataForLevels.levelsData[a_levelNo].requiredScoreToWin;
        m_livesLeft = m_dataForLevels.levelsData[a_levelNo].numberOfLives;
        GameEvents.updateSpawners?.Invoke();
        GameEvents.OnScoreUpdated?.Invoke(m_setScore);
        //GameEvents.OnLiveLost?.Invoke(m_livesLeft);
        GameEvents.OnTotalLivesUpdated?.Invoke(m_livesLeft);
        GameEvents.OnRequiredLevelScoreChanged?.Invoke(m_levelScore);
        GameManager.Instance.PauseTheGame(false);
    }

    public void LoadNextLevel()
    {
        m_levelToLoad++;

        if(m_levelToLoad < m_dataForLevels.levelsData.Length)
        {
            LoadLevel(m_levelToLoad);
        }
        else
        {
            GameManager.Instance.PauseTheGame(true);
            //m_gameSceneManagerRefrence.LoadMainScene();
            GamePlayUICanvas.SetGameWinPanelVisibility(true);
        }
    }

    public void ReloadLevel()
    {
        LoadLevel(m_levelToLoad);
    }

    private async Task PlayPoliceVisuals()
    {
        //while()
        //{

        //}
    }
}
