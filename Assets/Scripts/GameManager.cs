using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Chef chef;
    public Timer timer;

    [NonSerialized] public GameObject targetTool = null;
    [NonSerialized] public GameObject targetTeleport = null;
    [NonSerialized] public bool canDrob = false;
    [NonSerialized] public bool isDoneAllMissins = false;


    private static GameManager instance;
    public static GameManager Instance { get { return instance; } private set { } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<GameManager>();
        }
        else
            Destroy(gameObject);
    }
}
