using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    int m_mainSceneIndex;

    public void LoadMainScene()
    {
        _ = GameManager.Instance.LoadScene(m_mainSceneIndex);
    }
}