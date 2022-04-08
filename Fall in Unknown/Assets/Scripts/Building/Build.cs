using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Build : Building
{
    [HideInInspector] public Building prefabsBuilding = null;

    [SerializeField] private Collider collider = null;
    [SerializeField] private LayerMask mask;
    
    private float timer = 0f;
    public GameObject transfertUI = null;

    private bool IsPlace = false;
    public bool IsPlaceable = true;

    [SerializeField] private UIBuild prefabsUIBuild = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        transform.localScale = prefabsBuilding.transform.localScale;
        DelayBuild = prefabsBuilding.DelayBuild;

        maxLife = prefabsBuilding.maxLife / 4;
        Price() = prefabsBuilding.Price();

        Product = prefabsBuilding.Product;
        foreach (ResourceCounter resource in prefabsBuilding.ContiniousProduct)
        {
            ResourceCounter counter = new ResourceCounter();
            counter.resource = resource.resource;
            counter.quantity = resource.quantity;
            ContiniousProduct.Add(counter);
        }

        if (ContiniousProduct.Count > 0 || Product.Count > 0)
        {
            resourcesUI = Instantiate(ResourcesManager.instance.prefabsParentUIResource, GameManager.Instance.ParentUI);

            UIResource tmp2 = null;

            foreach (ResourceCounter uI in Product)
            {
                tmp2 = Instantiate(ResourcesManager.instance.prefabsResourceGroup, resourcesUI.transform);
                tmp2.ressource = uI;
            }

            foreach (ResourceCounter uI in ContiniousProduct)
            {
                tmp2 = Instantiate(ResourcesManager.instance.prefabsResourceGroup, resourcesUI.transform);
                tmp2.ressource = uI;
            }

            resourcesUI.sizeDelta = new Vector2(tmp2.GetComponent<RectTransform>().sizeDelta.x,
                tmp2.GetComponent<RectTransform>().sizeDelta.y * (Product.Count + ContiniousProduct.Count) + 20f * (Product.Count + ContiniousProduct.Count - 1));

            resourcesUI.gameObject.SetActive(true);
        }

        base.Awake();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (resourcesUI)
        {
            Vector3 screenPos = GameManager.Instance.cam.WorldToScreenPoint(transform.position) + new Vector3(0, 40);
            resourcesUI.transform.localPosition = new Vector3(screenPos.x - (Screen.width / 2), screenPos.y - (Screen.height / 2), 0f) / GameManager.Instance.canvas.scaleFactor;
        }

        if (IsPlace)
        {
            timer += Time.deltaTime;

            if (timer >= DelayBuild)
            {
                Building building = Instantiate(prefabsBuilding, BuildingManager.Instance.Parent);
                GameManager.Instance.MyEntity.Add(building);
                building.transform.position = transform.position;
                building.transform.rotation = transform.rotation;
                building.ContiniousProduct = new List<ResourceCounter>(ContiniousProduct);
                building.UI = transfertUI;

                Destroy(gameObject);
                if (resourcesUI)
                    Destroy(resourcesUI.gameObject);
                GameManager.Instance.MyEntity.Remove(this);
            }
        }
        else
        {
            for (int i = 0; i < ContiniousProduct.Count; i++)
            {
                if (!(ContiniousProduct[i].resource.type == ResourceSO.Type.Or && prefabsBuilding.name == "Habitation"))
                    ContiniousProduct[i].quantity = ContiniousProduct[i].CheckRessource(transform.position) * prefabsBuilding.ContiniousProduct[i].quantity;
            }

            IsPlaceable = true;
            Collider[] colliders = Physics.OverlapBox(transform.position, collider.bounds.size / 2.1f, transform.rotation, mask);
            if (colliders.Length > 1)
            {
                IsPlaceable = false;
            }
        }
    }

    public override void OnSelect()
    {
        if (!IsPlace)
            return;

        base.OnSelect();
        if (resourcesUI)
            resourcesUI.gameObject.SetActive(true);
    }

    public override void OnUnselect()
    {
        base.OnUnselect();
        if (resourcesUI)
            resourcesUI.gameObject.SetActive(false);
    }

    public void Placing()
    {
        IsPlace = true;
        gameObject.layer = LayerMask.NameToLayer("Player");

        UIBuild tmp = Instantiate(prefabsUIBuild, GameManager.Instance.ParentUI);
        tmp.transform.SetSiblingIndex(tmp.transform.parent.childCount - 2);
        tmp.owner = this;

        if (resourcesUI)
            resourcesUI.gameObject.SetActive(false);

        GameManager.Instance.MyEntity.Add(this);
    }

    public float Progress()
    {
        return timer / DelayBuild;
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0f, 90f, 0f));
    }
}
