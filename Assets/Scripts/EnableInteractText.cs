using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInteractText : MonoBehaviour
{
    public GameObject text;
    public bool isInteractable;
    public bool canGrab;

    void Start()
    {
        text.SetActive(false);
    }

    void Update()
    {
        if (isInteractable)
        {
            text.SetActive(true);
        }
        else
        {
            text.SetActive(false);
        }
    }
}
