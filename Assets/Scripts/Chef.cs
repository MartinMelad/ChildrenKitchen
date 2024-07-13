using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    [SerializeField] GameObject chefPositions;
    [SerializeField] AudioSource audioSource;


    Animator animator;
    bool isArrived = true;
    bool isTrunted = true;
    bool isEndTalkingOnce = true;
    Transform target;
    Quaternion initRotation;
    float speed = 1;
    float total = 0;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isArrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed* Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) < .03f)
            {
                animator.SetBool("IsWaking", false);
                //EventManager.Instance.onChefArrive.Invoke();

                isArrived = true;
            }
        }

        if (isArrived && !isTrunted) 
        {
            transform.rotation = Quaternion.Slerp(initRotation, target.rotation, total);
            total += Time.deltaTime;
            if (total >= 1)
            {
                EventManager.Instance.onChefArrive.Invoke();
                isTrunted = true;
            }
        }

        if (!audioSource.isPlaying && !isEndTalkingOnce)
        {
            animator.SetBool("IsTalking", false);
            EventManager.Instance.onChefEndTalking.Invoke();
            isEndTalkingOnce = true;
        }

    }

    public void SetDestination(int posNumber)
    {
        isArrived = false;
        isTrunted = false;
        target = chefPositions.transform.GetChild(posNumber);
        initRotation = target.rotation;
        total = 0;
        animator.SetBool("IsWaking", true);
        transform.LookAt(target);
    }

    public void Talk(AudioClip audio)
    {
        isEndTalkingOnce = false;
        animator.SetBool("IsTalking", true);
        audioSource.PlayOneShot(audio);
    }
}
