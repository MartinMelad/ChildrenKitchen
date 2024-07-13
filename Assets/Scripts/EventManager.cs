using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent<GameObject> tryPickUpObject;
    public UnityEvent<GameObject> tryTeleport;
    public UnityEvent<GameObject> onPickedUpObject;
    public UnityEvent<GameObject> onObjectDrobed;
    public UnityEvent<GameObject> onTeleported;
    public UnityEvent onChefArrive;
    public UnityEvent onChefEndTalking;
    public UnityEvent OnTimerEnd;
    public UnityEvent OnMissionDone;

    private static EventManager instance;
    public static EventManager Instance { get { return instance; } private set { } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<EventManager>();
        }
        else
            Destroy(gameObject);
    }
}
