using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsScript : MonoBehaviour
{
    public bool isDoingGrab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetToGrab()
    {
        isDoingGrab = true;
    }
    public void StopGrabbing()
    {
        isDoingGrab = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Interactable")
        {
            other.gameObject.GetComponent<EnableInteractText>().canGrab = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            other.gameObject.GetComponent<EnableInteractText>().canGrab = false;
        }
    }
}
