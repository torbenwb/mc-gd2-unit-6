using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PropPlacer))]
public class PropPlacerEditor : Editor
{
    public void OnSceneGUI()
    {
        Tools.current = Tool.None;
        HandleUtility.AddDefaultControl(0);

        // Get a world coordinate ray from the mouse position
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        // Raycast All returns all hits in a continuous direction
        RaycastHit[] hits = Physics.RaycastAll(ray);

        float minDistance = 1000f;

        // Iterate through each hit and find the first one
        // that belongs to a GameObject other than the prop
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != (target as PropPlacer).gameObject)
            {
                float distance = hit.distance;
                if (distance < minDistance){
                    (target as PropPlacer).transform.position = hit.point;
                    minDistance = distance;
                }
            }
        }

        if (Event.current.type == EventType.ScrollWheel)
        {
            Debug.Log("Mouse Wheel Event");
            Event.current.Use();

            float yawDelta = Event.current.delta.y * 5f;
            Vector3 eulerRotation = (target as PropPlacer).transform.rotation.eulerAngles;
            eulerRotation.y += yawDelta;

            (target as PropPlacer).transform.rotation = Quaternion.Euler(eulerRotation); 
        }

        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            GameObject newInstance = Instantiate((target as PropPlacer).gameObject);
            DestroyImmediate(newInstance.GetComponent<PropPlacer>());
        }
    }
}

public class PropPlacer : MonoBehaviour
{
    
}
