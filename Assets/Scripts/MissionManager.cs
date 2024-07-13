using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] GameObject[] toolsInArraing;
    [SerializeField] AudioClip[] arraingSounds;
    [SerializeField] GameObject[] teleportesInArraing;
    [SerializeField] AudioClip dropSound;
    [SerializeField] AudioClip Come;
    [SerializeField] AudioClip done;

    Queue<Mission> missionQueue;
    // Start is called before the first frame update
    void Start()
    {
        missionQueue = new Queue<Mission>();
        Mission mission = new ExploreMission(arraingSounds[0], dropSound, toolsInArraing[0]);
        missionQueue.Enqueue(mission);

        mission = new TeleportMission(teleportesInArraing[1], 1, Come);
        missionQueue.Enqueue(mission);

        mission = new ExploreMission(arraingSounds[1], dropSound, toolsInArraing[1]);
        missionQueue.Enqueue(mission);

        mission = new TeleportMission(teleportesInArraing[2], 2, Come);
        missionQueue.Enqueue(mission);

        mission = new ExploreMission(arraingSounds[2], dropSound, toolsInArraing[2]);
        missionQueue.Enqueue(mission);

        mission = new TeleportMission(teleportesInArraing[3], 3, Come);
        missionQueue.Enqueue(mission);

        mission = new ExploreMission(arraingSounds[3], dropSound, toolsInArraing[3]);
        missionQueue.Enqueue(mission);

        mission = new TeleportMission(teleportesInArraing[4], 4, Come);
        missionQueue.Enqueue(mission);

        mission = new ExploreMission(arraingSounds[4], dropSound, toolsInArraing[4]);
        missionQueue.Enqueue(mission);

        EventManager.Instance.OnMissionDone.AddListener(GoToNextMission);
        
    }


    void GoToNextMission()
    {
        if (missionQueue.Count > 0) 
        {
            missionQueue.Dequeue().StartMission();
        }
        else
        {
            GameManager.Instance.chef.Talk(done);
            GameManager.Instance.isDoneAllMissins = true;
        }
    }
}
