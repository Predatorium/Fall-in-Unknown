using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Ressources(Type _type, int _quantity)
    {
        type = _type;
        quantity = _quantity;
    }
}
