using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; set; }

    public Transform Hand;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
