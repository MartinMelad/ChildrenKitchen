using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] Transform pickUpPosition;
    [SerializeField] Transform interactopnObjects;
    [SerializeField] float moveSpeed;
    [SerializeField] float folwardSpeed;
    [SerializeField] float mouseSensitive;

    float lerpSpeed = 1f;
    Transform pickupObject;
    bool isLerped = false;
    bool isRotatting = false;
    bool isDrobing = false;

    float horizontalAngel;
    float verticalAngel;

    Vector3 initPos;
    Quaternion initRotation;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.tryPickUpObject.AddListener(TryPickUpObject);
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupObject != null)
        {
            MoveTowrdThePlayer();

            Move();

            Rotate();

            Drop();

        }
        
    }

    void MoveTowrdThePlayer()
    {
        if ( Vector3.Distance(pickUpPosition.position,pickupObject.position) > .01f && !isLerped)
        {
            pickupObject.position = Vector3.MoveTowards(pickupObject.position,
            pickUpPosition.position, lerpSpeed);

        }
        else
        {
            isLerped = true;
        }
    }

    void Move()
    {
        if (isLerped && !isRotatting)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            float floward = 0;
            if (Input.GetKey(KeyCode.Z))
                floward = 1;
            if (Input.GetKey(KeyCode.X))
                floward = -1;

            pickupObject.position += horizontalInput * pickUpPosition.right * moveSpeed * Time.deltaTime;
            pickupObject.position += verticalInput * pickUpPosition.up * moveSpeed * Time.deltaTime;
            pickupObject.position += floward * pickUpPosition.forward * moveSpeed * Time.deltaTime;
        }
    }

    void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");
            float turnCam = xInput * mouseSensitive;
            Vector3 currentRutation = pickupObject.eulerAngles;
            horizontalAngel += turnCam;
            if (horizontalAngel > 360)
                horizontalAngel -= 360;
            if (horizontalAngel < 0)
                horizontalAngel += 360;
            currentRutation.y = horizontalAngel;

            turnCam = -yInput * mouseSensitive;
            verticalAngel = Mathf.Clamp(verticalAngel + turnCam, -89f, 89);
            currentRutation.x = verticalAngel;

            pickupObject.transform.eulerAngles = currentRutation;
            isRotatting = true;
        }
        else
            isRotatting = false;
    }

    void Drop()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if ((!isDrobing && GameManager.Instance.canDrob)
                || (!isDrobing && GameManager.Instance.isDoneAllMissins))
            {
                pickupObject.parent = interactopnObjects;
                isDrobing = true;
            }
        }
        if (isDrobing)
        {
            if (Vector3.Distance(pickupObject.position, initPos) > .01f)
            {
                pickupObject.position = Vector3.MoveTowards(pickupObject.position,
                initPos, lerpSpeed*Time.deltaTime);

            }
            else
            {
                isDrobing = false;
                EventManager.Instance.onObjectDrobed.Invoke(pickupObject.gameObject);
                pickupObject.transform.rotation = transform.rotation;
                pickupObject = null;
                this.enabled = false;
            }
        }
    }
    void TryPickUpObject(GameObject tool)
    {
        if ((tool == GameManager.Instance.targetTool && pickupObject == null) ||
            (GameManager.Instance.isDoneAllMissins && pickupObject == null))
        {
            this.enabled = true;
            isLerped = false;
            pickupObject = tool.transform;
            pickupObject.parent = pickUpPosition;
            horizontalAngel = pickupObject.eulerAngles.y;
            verticalAngel = pickupObject.eulerAngles.x;
            initPos = tool.transform.position;
            initRotation = tool.transform.rotation;
            EventManager.Instance.onPickedUpObject.Invoke(tool);
        }
    }

}
