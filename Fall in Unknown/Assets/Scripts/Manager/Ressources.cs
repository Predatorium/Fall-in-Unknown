using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Ressources
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
    public int quantity = 0;
    public Sprite sprite = null;

    public int CheckRessource(Vector3 position)
    {
        return Physics.OverlapSphere(position, 30f, 1 << LayerMask.NameToLayer("Resource")).Select(c => c.CompareTag(type.ToString())).Count();
    }
}
