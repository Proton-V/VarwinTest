using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public GameObject TableActionItem;
    public bool IsActive = false;
    private CanvasManager _canvasManager;

    private void Start()
    {
        _canvasManager = CanvasManager.Instance;
    }
    private void Update()
    {
        if (IsActive)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerController.Instance.TakePistol(TableActionItem);
                Destroy(this);
            }
        }
    }
    public void TableActionItemMsg()
    {
        _canvasManager.SetTextMsg("Press F to Take Item");
    }

}
