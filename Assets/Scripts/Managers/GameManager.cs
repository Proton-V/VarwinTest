using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    [HideInInspector]
    public List<BulletController> SpawnBullets = new List<BulletController>();
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

}
