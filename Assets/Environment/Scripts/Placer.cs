using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Placer))]
public class PlacerEditor : Editor
{

    public void OnSceneGUI()
    {
        Tools.current = Tool.None;
        HandleUtility.AddDefaultControl(0);
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            Debug.Log($"Hit {hit.collider.gameObject.name}");
            (target as Placer).transform.position = hit.point;
            (target as Placer).transform.rotation = Quaternion.LookRotation(hit.normal);

            if (Event.current.isMouse)
            {
                if (Event.current.button == 0 && Event.current.type == EventType.MouseDown)
                {
                    Debug.Log("Click");
                    Event.current.Use();
                    GameObject newInstance = Instantiate((target as Placer).gameObject);
                    
                    DestroyImmediate(newInstance.GetComponent<Placer>());
                }
            }
        }
        else
        {
            Debug.Log("No hit?");
        }
    }
}

public class Placer : MonoBehaviour
{
    
}
