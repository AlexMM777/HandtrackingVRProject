using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonLookAtTarget : MonoBehaviour
{
    public Transform target;
    public Transform defaultTarget;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(target);
        transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);

        /*
        if (Vector3.Distance(this.transform.position, target.position) < 50)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.LookAt(defaultTarget);
        }*/
    }
}
