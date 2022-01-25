using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance = null;

    private List<Building> buildings = new List<Building>();

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

    public void AddBuilding(Building building)
    {
        if (buildings.Where(b => (b.gameObject == building.gameObject)).Count() == 0)
        {
            buildings.Add(building);
            building.OnSelect();
        }
    }

    public void ClearBuildings()
    {
        while (buildings.Count > 0)
        {
            buildings[0].OnUnselect();
            buildings.Remove(buildings[0]);
        }
    }

    public void RecrutUnit(string name)
    {
        RessourcesManager.Instance.BuyingEntity(name);
    }
}
