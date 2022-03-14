using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class RessourcesManager : MonoBehaviour
{
    public static RessourcesManager Instance = null;
    public Ressources[] ressources = null;

    [SerializeField] private TextMeshProUGUI[] resourceNb = null;
    [SerializeField] private List<Entity> prefabsEntities = null;

    public Transform prefabsParentUIResource = null;
    public UIResource prefabsResourceGroup = null;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResourcesUpdate()
    {
        for (int i = 0; i < ressources.Length; i++)
        {
            resourceNb[i].text = ressources[i].quantity.ToString();
        }
    }

    public Entity BuyingEntity(string name)
    {
        int index = prefabsEntities.IndexOf(prefabsEntities.Where(e => (e.name == name)).First());
        if (prefabsEntities[index].Buyable(ref ressources))
        {
            return prefabsEntities[index];
        }

        return null;
    }

    public void Purchase(ref Ressources[] price)
    {
        foreach (Ressources p in price)
        {
            Ressources tmp = ressources.Where(r => r.type == p.type).First();
            int index = System.Array.IndexOf(ressources, tmp);
            ressources[index].quantity -= p.quantity;
        }

        ResourcesUpdate();
    }

    public void Sell(ref Ressources[] price)
    {
        foreach (Ressources p in price)
        {
            Ressources tmp = ressources.Where(r => r.type == p.type).First();
            int index = System.Array.IndexOf(ressources, tmp);
            ressources[index].quantity += p.quantity;
        }

        ResourcesUpdate();
    }
}
