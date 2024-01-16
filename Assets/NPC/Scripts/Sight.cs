using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class Sight : MonoBehaviour
{
    public GameObject target;
    public float angle = 60f;
    public float range = 20f;

    public bool CanSeeTarget()
    {
        if (!target) return false;
        Vector3 toTarget = target.transform.position - transform.position;
        if (toTarget.magnitude > range) return false;
        if (Vector3.Angle(transform.forward, toTarget) > angle) return false;
        if (Physics.Raycast(transform.position, toTarget, out var hit))
        {
            if (hit.collider.gameObject == target) return true;
            else return false;
        }
        return true;
    }

    void OnDrawGizmos()
    {
        //Handles.Label(transform.position, CanSeeTarget().ToString());
    }
}
