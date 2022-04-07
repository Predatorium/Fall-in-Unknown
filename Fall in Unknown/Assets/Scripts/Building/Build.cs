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
        ContiniousProduct = prefabsBuilding.ContiniousProduct;

        if (ContiniousProduct.Length > 0 || Product.Length > 0)
        {
            resourcesUI = Instantiate(ResourcesManager.Instance.prefabsParentUIResource, GameManager.Instance.ParentUI);

            UIResource tmp2 = null;

            foreach (Resource uI in Product)
            {
                tmp2 = Instantiate(ResourcesManager.Instance.prefabsResourceGroup, resourcesUI.transform);
                tmp2.ressource = uI;
            }

            foreach (Resource uI in ContiniousProduct)
            {
                tmp2 = Instantiate(ResourcesManager.Instance.prefabsResourceGroup, resourcesUI.transform);
                tmp2.ressource = uI;
            }

            resourcesUI.sizeDelta = new Vector2(tmp2.GetComponent<RectTransform>().sizeDelta.x,
                tmp2.GetComponent<RectTransform>().sizeDelta.y * (Product.Length + ContiniousProduct.Length) + 20f * (Product.Length + ContiniousProduct.Length - 1));

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

            foreach (Resource ressource in ContiniousProduct)
            {
                if (ressource.type != Resource.Type.Or)
                    ressource.quantity = ressource.CheckRessource(transform.position);
            }
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
                building.ContiniousProduct = ContiniousProduct;
                building.UI = transfertUI;

                Destroy(gameObject);
                Destroy(resourcesUI.gameObject);
                GameManager.Instance.MyEntity.Remove(this);
            }
        }
        else
        {   
            IsPlaceable = true;
            Collider[] colliders = Physics.OverlapBox(transform.position, collider.bounds.size / 2f, transform.rotation, mask);
            if (colliders.Length > 1)
            {
                IsPlaceable = false;
            }
        }
    }

    public void Placing()
    {
        IsPlace = true;
        gameObject.layer = LayerMask.NameToLayer("Player");

        UIBuild tmp = Instantiate(prefabsUIBuild, GameManager.Instance.ParentUI);
        tmp.owner = this;
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
