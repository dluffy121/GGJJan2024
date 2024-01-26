using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUICanvas : MonoBehaviour
{
    [SerializeField]
    RectTransform LevelCompletePanel, LevelFailedPanel;

    private static GamePlayUICanvas m_instance;

    private void Awake()
    {
        m_instance = this;
    }

    public void OnLoadNextLevelPressed()
    {
        LevelManager.Instance.LoadNextLevel();
        SetLevelCompletePanelVisibility(false);
    }

    public void OnReloadLevelPressed()
    {
        LevelManager.Instance.ReloadLevel();
        SetLevelFailedPanelVisibility(false);
    }

    public static void SetLevelCompletePanelVisibility(bool a_visibility)
    {
        m_instance.LevelCompletePanel.gameObject.SetActive(a_visibility);
    }

    public static void SetLevelFailedPanelVisibility(bool a_visibility)
    {
        m_instance.LevelFailedPanel.gameObject.SetActive(a_visibility);
    }
}
