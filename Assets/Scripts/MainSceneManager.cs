using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    int m_gameSceneIndex;

    public void LoadGameScene()
    {
        _ = GameManager.Instance.LoadScene(m_gameSceneIndex);
    }

    public void PlayClickSound()
    {
        SoundManager.Instance.PlayClickSound();
    }
}