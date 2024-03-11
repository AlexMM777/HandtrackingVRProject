using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandCollision : MonoBehaviour
{
    public XRBaseInteractable selectTarget { get; }
    public XRGrabInteractable script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(script.isSelected)
        {
            print(script.isSelected);
        }
        
    }
}
