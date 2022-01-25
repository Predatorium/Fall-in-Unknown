using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ressources
{
    public enum Type
    {
        Population,
        Energie,
        Bois,
        Fer,
        Or,
    }

    public Type type = Type.Population;
    public int quantity = 0;
    
    public Ressources(Type _type, int _quantity)
    {
        type = _type;
        quantity = _quantity;
    }
}
