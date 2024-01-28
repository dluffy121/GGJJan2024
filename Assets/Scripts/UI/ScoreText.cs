using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private Text m_scoreText;
    [SerializeField]
    private Text m_requiredScoreToWin;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.OnRequiredLevelScoreChanged += CallBackOnRequiredLevelScoreChanged;
        //m_scoreText = gameObject.GetComponent<Text>();
        m_scoreText.text = string.Format("CURRENT MONEY : {0}",0);
    }

    private void OnEnable()
    {
        GameEvents.OnScoreUpdated += CallbackOnScoreUpdated;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreUpdated -= CallbackOnScoreUpdated;
    }

    private void OnDestroy()
    {
        GameEvents.OnRequiredLevelScoreChanged -= CallBackOnRequiredLevelScoreChanged;
    }

    private void CallbackOnScoreUpdated(int a_Score)
    {
        m_scoreText.text = string.Format("CURRENT MONEY : {0}",a_Score);
    }

    private void CallBackOnRequiredLevelScoreChanged(int a_requiredScore)
    {
        m_requiredScoreToWin.text = string.Format("REQUIRED MONEY : {0}", a_requiredScore);
    }
}
