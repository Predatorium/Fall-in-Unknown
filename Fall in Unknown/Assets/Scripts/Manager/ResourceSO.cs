using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Create/Resource")]
public class ResourceSO : ScriptableObject
{
    public enum Type
    {
        Population,
        Nourriture,
        Energie,
        Bois,
        Fer,
        Or,
    }
    public Type type = Type.Population;
    public Sprite sprite = null;
}
