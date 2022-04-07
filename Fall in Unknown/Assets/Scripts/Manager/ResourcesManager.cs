using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

[System.Serializable]
public class Resource
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
        return Physics.OverlapSphere(position, 30f, 1 << LayerMask.NameToLayer("Resource")).Where(c => c.CompareTag(type.ToString())).Count();
    }
}

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance = null;
    public Resource[] ressources = null;

    [SerializeField] private TextMeshProUGUI[] resourceNb = null;
    [SerializeField] private List<Entity> prefabsEntities = null;

    public RectTransform prefabsParentUIResource = null;
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

    public void Purchase(ref Resource[] price)
    {
        foreach (Resource p in price)
        {
            Resource tmp = ressources.Where(r => r.type == p.type).First();
            int index = System.Array.IndexOf(ressources, tmp);
            ressources[index].quantity -= p.quantity;
        }

        ResourcesUpdate();
    }

    public void Sell(ref Resource[] price)
    {
        foreach (Resource p in price)
        {
            Resource tmp = ressources.Where(r => r.type == p.type).First();
            int index = System.Array.IndexOf(ressources, tmp);
            ressources[index].quantity += p.quantity;
        }

        ResourcesUpdate();
    }
}
