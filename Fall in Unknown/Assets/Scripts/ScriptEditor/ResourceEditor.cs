using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Entity), true)]
public class ResourceEditor : Editor
{
    Entity obj = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Find sprite resource"))
        {
            obj = serializedObject.targetObject as Entity;

            foreach (Resource resource in obj.price)
            {
                resource.sprite = FindObjectOfType<ResourcesManager>().ressources.Where(r => r.type == resource.type).First().sprite;
            }

            Building building = obj as Building;

            if (building)
            {
                foreach (Resource resource in building.Product)
                {
                    resource.sprite = FindObjectOfType<ResourcesManager>().ressources.Where(r => r.type == resource.type).First().sprite;
                }   

                foreach (Resource resource in building.ContiniousProduct)
                {
                    resource.sprite = FindObjectOfType<ResourcesManager>().ressources.Where(r => r.type == resource.type).First().sprite;
                }
            }
        }
    }
}
