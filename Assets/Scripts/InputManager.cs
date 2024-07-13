using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] Transform cursor;
    [SerializeField] float maxRayDistanc;
    [SerializeField] GameObject canva;
    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canva.SetActive(!canva.activeInHierarchy);
        }


        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = new Ray(cursor.position, cursor.forward);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, maxRayDistanc))
            {

                if (hit.collider.CompareTag("Interactable"))
                {
                    EventManager.Instance.tryPickUpObject.Invoke(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Teleport"))
                    EventManager.Instance.tryTeleport.Invoke(hit.collider.gameObject);
                else if (hit.collider.CompareTag("Button"))
                {
                    hit.collider.GetComponent<Button>().onClick.Invoke();
                }

            }
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        line.SetPosition(0,cursor.position);
        line.SetPosition(1, (cursor.position + (cursor.forward * maxRayDistanc)));

        
    }
}
