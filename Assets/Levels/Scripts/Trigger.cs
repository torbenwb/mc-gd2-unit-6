using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Trigger : MonoBehaviour
{
    Entity entity;

    void Awake() => entity = GetComponent<Entity>();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) entity.ToggleActive();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) entity.ToggleActive();
    }
}
