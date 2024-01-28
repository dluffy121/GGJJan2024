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
    [SerializeField]
    MoneyUIEffect moneyAddUIPrefab;
    [SerializeField]
    private RectTransform m_rectTrans;

    int m_score;

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
        int l_diffenceInScore = a_Score - m_score;
        m_scoreText.text = string.Format("CURRENT MONEY : {0}",a_Score);
        MoneyUIEffect l_effect = Instantiate<MoneyUIEffect>(moneyAddUIPrefab, m_rectTrans.position, m_rectTrans.rotation, transform);
        l_effect.StartAnimation(l_diffenceInScore);
    }

    private void CallBackOnRequiredLevelScoreChanged(int a_requiredScore)
    {
        m_requiredScoreToWin.text = string.Format("REQUIRED MONEY : {0}", a_requiredScore);
    }
}
