using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private Text m_scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //m_scoreText = gameObject.GetComponent<Text>();
        m_scoreText.text = string.Format("Score : {0}",0);
    }

    private void OnEnable()
    {
        GameEvents.OnScoreUpdated += CallbackOnScoreUpdated;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreUpdated -= CallbackOnScoreUpdated;
    }

    private void CallbackOnScoreUpdated(int a_Score)
    {
        m_scoreText.text = string.Format("Score : {0}",a_Score);
    }
}
