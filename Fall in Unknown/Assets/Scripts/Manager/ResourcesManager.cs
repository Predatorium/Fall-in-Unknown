using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

[System.Serializable]
public class ResourceCounter
{
    public ResourceSO resource = null;
    public int quantity = 0;

    public int CheckRessource(Vector3 position)
    {
        return Physics.OverlapSphere(position, 12f, 1 << LayerMask.NameToLayer("Resource")).Where(c => c.CompareTag(resource.type.ToString())).Count();
    }
}

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager instance = null;
    public List<ResourceCounter> resources = null;

    [SerializeField] private TextMeshProUGUI[] resourceNb = null;
    [SerializeField] private List<Entity> prefabsEntities = null;

    public RectTransform prefabsParentUIResource = null;
    public UIResource prefabsResourceGroup = null;

    private void Awake()
    {
        instance = this;
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
        for (int i = 0; i < resources.Count; i++)
        {
            resourceNb[i].text = resources[i].quantity.ToString();
        }
    }

    public Entity BuyingEntity(string name)
    {
        int index = prefabsEntities.IndexOf(prefabsEntities.Where(e => (e.name == name)).First());
        if (prefabsEntities[index].Buyable(ref resources))
        {
            return prefabsEntities[index];
        }

        return null;
    }

    public void Purchase(ref List<ResourceCounter> price)
    {
        foreach (ResourceCounter p in price)
        {
            ResourceCounter tmp = resources.Where(r => r.resource.type == p.resource.type).First();
            int index = resources.IndexOf(tmp);
            resources[index].quantity -= p.quantity;
        }

        ResourcesUpdate();
    }

    public void Sell(ref List<ResourceCounter> price)
    {
        foreach (ResourceCounter p in price)
        {
            ResourceCounter tmp = resources.Where(r => r.resource.type == p.resource.type).First();
            int index = resources.IndexOf(tmp);
            resources[index].quantity += p.quantity;
        }

        ResourcesUpdate();
    }
}
