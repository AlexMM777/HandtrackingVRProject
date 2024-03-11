using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractebleItems : MonoBehaviour
{
    public Transform[] interacItemsTrans;

    void Start()
    {
        
    }

    void Update()
    {
        if (IsVisible(GetClosestInteractable(interacItemsTrans).transform.GetChild(1).gameObject))
        {
            GetClosestInteractable(interacItemsTrans).gameObject.GetComponent<EnableInteractText>().isInteractable = true;
        }
    }

    Transform GetClosestInteractable(Transform[] interactables)
    {
        //print("RUNNING...");
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in interactables)
        {
            t.gameObject.GetComponent<EnableInteractText>().isInteractable = false;
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    private bool IsVisible(GameObject item)
    {
        if (item.GetComponent<Renderer>().isVisible)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
