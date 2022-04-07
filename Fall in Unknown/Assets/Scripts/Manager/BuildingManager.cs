using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class UIBuilding
{
    public string Name = "";
    public GameObject UI = null;
}

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance = null;

    private List<Building> buildings = new List<Building>();

    private GameObject UI = null;

    [SerializeField] private UIBuilding[] UIs = null;

    [SerializeField] private Build prefabBuild = null;
    private Build build = null;

    public Transform Parent = null;

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
        if (build)
        {
            build.transform.position = Raycast() + build.transform.localScale / 2f + new Vector3(0f, 0.001f, 0f);

            if (Input.GetMouseButtonDown(0) && build.IsPlaceable)
            {
                build.Placing();
                ResourcesManager.Instance.Purchase(ref build.prefabsBuilding.Price());
                build = null;
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(build.gameObject);
                build = null;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                build.Rotate();
            }
        }
    }

    public void AddBuilding(Building building)
    {
        if (!buildings.Contains(building))
        {
            buildings.Add(building);
            building.OnSelect();

            UI = building.UI;
            UI.SetActive(true);
        }
    }

    public void ClearBuildings()
    {
        while (buildings.Count > 0)
        {
            buildings[0].OnUnselect();
            buildings.Remove(buildings[0]);

            if (UI)
            {
                UI.gameObject.SetActive(false);
                UI = null;
            }
        }
    }

    public void RecrutUnit(string name)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            buildings[i].recrut.RecrutingUnit(name);
        }
    }

    public void ConstructBuilding(string name)
    {
        Building building = ResourcesManager.Instance.BuyingEntity(name) as Building;
        if (building)
        {
            build = Instantiate(prefabBuild, Parent);
            GameManager.Instance.MyEntity.Add(build);
            build.prefabsBuilding = building;
            build.UI = UIs.Where(u => u.Name == "Build").First().UI;
            build.transfertUI = UIs.Where(u => u.Name == name).First().UI;
        }
    }

    public void DestroyBuildings()
    {
        while (buildings.Count > 0)
        {
            Building tmp = buildings[0];
            ResourcesManager.Instance.Sell(ref tmp.Price());
            buildings.Remove(buildings[0]);
            Destroy(tmp.gameObject);
        }

        UI.gameObject.SetActive(false);
        UI = null;
    }

    private Vector3 Raycast()
    {
        Ray ray = GameManager.Instance.cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~((1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Build")))))
        {
            return new Vector3((int)(hit.point.x / 2f) * 2f, hit.point.y, (int)(hit.point.z / 2f) * 2f);
        }

        return Vector3.zero;
    }
}
