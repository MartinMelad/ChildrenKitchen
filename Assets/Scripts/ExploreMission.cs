using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreMission : Mission
{
    AudioClip pickingAudio;
    AudioClip dropAdue;
    GameObject tool;
    public ExploreMission(AudioClip pickingAudio, AudioClip dropAdue, GameObject tool)
    {
        this.pickingAudio = pickingAudio;
        this.dropAdue = dropAdue;
        this.tool = tool;
    }
    public override void StartMission()
    {
        PickingTalk();
    }

    void PickingTalk()
    {
        GameManager.Instance.chef.Talk(pickingAudio);
        EventManager.Instance.onChefEndTalking.AddListener(OnEndPickingTalk);
    }

    void OnEndPickingTalk()
    {
        EventManager.Instance.onChefEndTalking.RemoveListener(OnEndPickingTalk);
        GameManager.Instance.targetTool = this.tool;
        EventManager.Instance.onPickedUpObject.AddListener(OnPlyerPickTool);
    }

    void OnPlyerPickTool(GameObject tool)
    {
        if (tool == this.tool)
        {
            EventManager.Instance.onPickedUpObject.RemoveListener(OnPlyerPickTool);
            GameManager.Instance.timer.StartTimer(5);
            EventManager.Instance.OnTimerEnd.AddListener(DropTalk);
        }

    }

    void DropTalk()
    {

        EventManager.Instance.OnTimerEnd.RemoveListener(DropTalk);
        GameManager.Instance.chef.Talk(dropAdue);
        EventManager.Instance.onChefEndTalking.AddListener(EnableDrop);
    }

    void EnableDrop()
    {
        EventManager.Instance.onChefEndTalking.RemoveListener(EnableDrop);
        GameManager.Instance.canDrob = true;
        EventManager.Instance.onObjectDrobed.AddListener(OnDropTool);
    }

    void OnDropTool(GameObject tool)
    {
        if (tool == this.tool) 
        {
            EventManager.Instance.onObjectDrobed.RemoveListener(OnDropTool);
            GameManager.Instance.canDrob = false;
            GameManager.Instance.targetTool = null;
            EventManager.Instance.OnMissionDone.Invoke();
        } 
          
    }


}
