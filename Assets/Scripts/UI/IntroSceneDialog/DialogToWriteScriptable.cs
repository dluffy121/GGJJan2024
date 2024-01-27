using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogsToWriteSO", menuName = "ScriptableObject/DialogsToWrite")]
public class DialogToWriteScriptable : ScriptableObject
{
    public string[] m_dialogsToWrite;
}
