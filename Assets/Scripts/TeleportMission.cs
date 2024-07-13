using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportMission : Mission
{
    GameObject teleport;
    int teleportNumber;
    AudioClip audio;

    public TeleportMission(GameObject teleport, int teleportNumber, AudioClip audio)
    {
        this.teleport = teleport;
        this.teleportNumber = teleportNumber;
        this.audio = audio;
    }

    public override void StartMission()
    {
        MoveChef();
    }

    void MoveChef()
    {
        GameManager.Instance.chef.SetDestination(teleportNumber);
        EventManager.Instance.onChefArrive.AddListener(Talk);
    }

    void Talk()
    {
        EventManager.Instance.onChefArrive.RemoveListener(Talk);
        GameManager.Instance.chef.Talk(audio);
        EventManager.Instance.onChefEndTalking.AddListener(EnableTeleport);
    }

    void EnableTeleport()
    {
        EventManager.Instance.onChefEndTalking.RemoveListener(EnableTeleport);
        GameManager.Instance.targetTeleport = this.teleport;
        EventManager.Instance.onTeleported.AddListener(OnTeleported);
    }

    void OnTeleported(GameObject teleport)
    {
        if (teleport == this.teleport) 
        {
            EventManager.Instance.onTeleported.RemoveListener(OnTeleported);
            GameManager.Instance.targetTeleport = null;
            EventManager.Instance.OnMissionDone.Invoke();
        }
    }

}
