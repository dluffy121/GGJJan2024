using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour
{
    private Text m_livesText;
    // Start is called before the first frame update
    void Start()
    {
        m_livesText = gameObject.GetComponent<Text>();
        m_livesText.text = string.Format("Lives : {0}",5);
    }

    private void OnEnable()
    {
        GameEvents.OnLiveLost += CallbackOnLiveLost;
    }

    private void OnDisable()
    {
        GameEvents.OnLiveLost -= CallbackOnLiveLost;
    }

    private void CallbackOnLiveLost(int a_Lives)
    {
        m_livesText.text = string.Format("Lives : {0}", a_Lives);
    }
}
