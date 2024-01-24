using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager s_instance = null;

    AsyncOperation m_asyncOperation;

    public static GameManager Instance => s_instance;

    public float LoadingProgress => m_asyncOperation == null ? float.NaN : m_asyncOperation.progress;

    private void Awake()
    {
        s_instance = this;
    }

    private void OnDestroy()
    {
        s_instance = null;
    }

    public async Task LoadScene(int a_sceneIndex)
    {
        m_asyncOperation = SceneManager.LoadSceneAsync(a_sceneIndex);

        while (!m_asyncOperation.isDone)
        {
            // Use this to show loading progress bar
            float l_progress = m_asyncOperation.progress;
            await Task.Yield();
        }

        m_asyncOperation = null;
    }
}
