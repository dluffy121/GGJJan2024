using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    int m_mainSceneIndex;

    public void LoadMainScene()
    {
        GameManager.Instance.PauseTheGame(false);
        _ = GameManager.Instance.LoadScene(m_mainSceneIndex);
    }

    public void PlayClickSound()
    {
        SoundManager.Instance.PlayClickSound();
    }
}