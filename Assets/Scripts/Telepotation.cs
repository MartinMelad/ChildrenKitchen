using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepotation : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.tryTeleport.AddListener(TryTeleport);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TryTeleport(GameObject teleport)
    {
        GameObject targetTeleport = GameManager.Instance.targetTeleport;
        if (teleport == targetTeleport || GameManager.Instance.isDoneAllMissins)
        {
            Vector3 offSide = new Vector3 (0, .75f, 0);
            player.position = teleport.transform.position + offSide;
            player.rotation = teleport.transform.rotation;
            EventManager.Instance.onTeleported.Invoke(teleport);
            GameManager.Instance.targetTeleport = null;
        }
    }
}
