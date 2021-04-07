using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; set; }

    public TextMeshProUGUI MsgText;
    public PistolUIManager PistolUIManager;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void SetTextMsg(string msg)
    {
        MsgText.text = msg;
    }
    public void ClearTextMsg()
    {
        MsgText.text = "";
    }
}
