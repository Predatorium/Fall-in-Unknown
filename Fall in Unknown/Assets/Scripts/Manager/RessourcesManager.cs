using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RessourcesManager : MonoBehaviour
{
    public static RessourcesManager Instance = null;
    public List<Ressources> ressources = null;

    [SerializeField] private List<Entity> prefabsEntities = null;

    private void Awake()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Entity BuyingEntity(string name)
    {
        int index = prefabsEntities.IndexOf(prefabsEntities.Where(e => (e.name == name)).First());
        if (prefabsEntities[index].Buyable(ref ressources))
        {
            List<Ressources> resources = prefabsEntities[index].Price();
            Purchase(ref resources);
            return Instantiate(prefabsEntities[index]);
        }

        return null;
    }

    private void Purchase(ref List<Ressources> price)
    {
        foreach (Ressources p in price)
        {
            int index = ressources.IndexOf(ressources.Where(r => (r.type == p.type)).First());
            ressources[index].quantity -= p.quantity;
        }
    }
}
