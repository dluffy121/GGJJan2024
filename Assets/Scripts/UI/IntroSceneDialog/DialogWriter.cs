using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;

public class DialogWriter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_dialogTetx;
    [SerializeField]
    Button m_nextDialogButton;
    [SerializeField]
    DialogToWriteScriptable m_dialogsScritableObjectRef;
    [SerializeField]
    int m_maxLengthOfDialog = 30;
    [SerializeField]
    float m_waitToWriteNextLetter = 0.1f;
    [SerializeField]
    AudioClip m_typeWriterClip, m_glassBreakSound;

    [SerializeField]
    GameObject InstructionsPanel;

    int m_indexOfDialogToWrite = 0;
    StringBuilder m_stringBuilder;
    bool isWriting;
    float currentTime;
    int m_indexOfLetterToWrite;
    int m_lengthOfADialog;
    string m_currentDialogToWrite;

    private void Start()
    {
        m_stringBuilder = new StringBuilder(m_maxLengthOfDialog);
        WriteTheNextDialog();
    }

    public void WriteTheNextDialog()
    {
        SoundManager.Instance.PlayClickSound();
        if (m_indexOfDialogToWrite >= m_dialogsScritableObjectRef.m_dialogsToWrite.Length)
        {
            //sound of glass breaking
            SoundManager.PlaySoundEffect(m_glassBreakSound);
            InstructionsPanel.gameObject.SetActive(true);
        }
        else
        {
            isWriting = true;
            currentTime = 0;
            m_indexOfLetterToWrite = 0;
            m_currentDialogToWrite = m_dialogsScritableObjectRef.m_dialogsToWrite[m_indexOfDialogToWrite];
            m_lengthOfADialog = m_currentDialogToWrite.Length;
            m_nextDialogButton.interactable = false;
        }
    }

    private void OnDialogGotWritten()
    {
        //Activate a button
        isWriting = false;
        m_indexOfDialogToWrite++;
        m_nextDialogButton.interactable = true;
        m_stringBuilder.Clear();
    }

    private void WriteTheDialogText()
    {
        m_stringBuilder.Append(m_currentDialogToWrite[m_indexOfLetterToWrite]);
        m_indexOfLetterToWrite++;
        m_dialogTetx.text = m_stringBuilder.ToString();
        SoundManager.PlaySoundEffect(m_typeWriterClip);
    }

    private void ForceWriteTheWholeDialog()
    {
        m_stringBuilder.Clear();
        m_dialogTetx.text = m_dialogsScritableObjectRef.m_dialogsToWrite[m_indexOfDialogToWrite];
        OnDialogGotWritten();
    }

    private void Update()
    {
        if(isWriting)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= m_waitToWriteNextLetter)
            {
                WriteTheDialogText();
                currentTime = 0;
                if(m_indexOfLetterToWrite >= m_lengthOfADialog)
                {
                    OnDialogGotWritten();
                }
            }
        }

        //if(Input.GetKeyDown(KeyCode.Return))
        //{
          //  ForceWriteTheWholeDialog();
        //}
    }

    public void LoadGamePlayScene()
    {
        _ = GameManager.Instance.LoadScene(2);
    }
}
