using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BNG;


[CustomEditor(typeof(Transform))]
public class Mrmay5 : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

      Transform t = (Transform)target;
        GameObject gameObject = t.gameObject;

        if (GUILayout.Button("Snap Zone Slot"))
        {
            
            
            // Check if Script1 is already attached
            if (gameObject.GetComponent<Grabbable>() == null)
            {
                gameObject.AddComponent<Grabbable>();
                
            }

            // Check if Script2 is already attached
            if (gameObject.GetComponent<SnapZone>() == null)
            {
                gameObject.AddComponent<SnapZone>();

            }
            
            // Check if Script2 is already attached
            if (gameObject.GetComponent<GrabbablesInTrigger>() == null)
            {
                gameObject.AddComponent<GrabbablesInTrigger>();
               
            }
            
            // Check if Script2 is already attached
            if (gameObject.GetComponent<GrabAction>() == null)
            {
                gameObject.AddComponent<GrabAction>();
               
            }
        }

        if (GUILayout.Button("Grabbales"))
        {

           

            // Check if Script2 is already attached
            if (gameObject.GetComponent<Rigidbody>() == null)
            {
                gameObject.AddComponent<Rigidbody>();

            }

            // Check if Script2 is already attached
            if (gameObject.GetComponent<BoxCollider>() == null)
            {
                gameObject.AddComponent<BoxCollider>();

            }
            // Check if Script1 is already attached
            if (gameObject.GetComponent<Grabbable>() == null)
            {
                gameObject.AddComponent<Grabbable>();

            }

        }

       /* if(GUILayout.Button(""))*/

    }
}
