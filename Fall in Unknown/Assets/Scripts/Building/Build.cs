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

    protected override void Awake()
    {

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        transform.localScale = prefabsBuilding.transform.localScale;
        DelayBuild = prefabsBuilding.DelayBuild;

        maxLife = prefabsBuilding.maxLife / 4;
        Price() = prefabsBuilding.Price();

        Product = prefabsBuilding.Product;
        ContiniousProduct = prefabsBuilding.ContiniousProduct;

        base.Awake();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (IsPlace)
        {
            timer += Time.deltaTime;

            if (resourcesUI)
            {
                Vector3 screenPos = GameManager.Instance.cam.WorldToScreenPoint(transform.position) + new Vector3(0, 40);
                resourcesUI.transform.localPosition = new Vector3(screenPos.x - (Screen.width / 2), screenPos.y - (Screen.height / 2), 0f) / GameManager.Instance.canvas.scaleFactor;
            }

            if (timer >= DelayBuild)
            {
                Building building = Instantiate(prefabsBuilding, BuildingManager.Instance.Parent);
                GameManager.Instance.MyEntity.Add(building);
                building.transform.position = transform.position;
                building.transform.rotation = transform.rotation;
                building.UI = transfertUI;

                Destroy(gameObject);
                Destroy(resourcesUI.gameObject);
                GameManager.Instance.MyEntity.Remove(this);
            }
        }
        else
        {   
            IsPlaceable = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, collider.bounds.size.x * 2f, mask);
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

        if (ContiniousProduct.Length > 0 || Product.Length > 0)
        {
            resourcesUI = Instantiate(RessourcesManager.Instance.prefabsParentUIResource, GameManager.Instance.ParentUI);

            UIResource tmp2 = null;

            foreach (Ressources uI in Product)
            {
                tmp2 = Instantiate(RessourcesManager.Instance.prefabsResourceGroup, resourcesUI.transform);
                tmp2.text.text = "+" + uI.quantity;
                tmp2.image.sprite = RessourcesManager.Instance.ressources.Where(o => o.type == uI.type).First().sprite;
            }

            foreach (Ressources uI in ContiniousProduct)
            {
                tmp2 = Instantiate(RessourcesManager.Instance.prefabsResourceGroup, resourcesUI.transform);
                tmp2.text.text = "+" + uI.quantity;
                tmp2.image.sprite = RessourcesManager.Instance.ressources.Where(o => o.type == uI.type).First().sprite;
            }

            resourcesUI.sizeDelta = new Vector2(tmp2.text.rectTransform.sizeDelta.x + tmp2.image.rectTransform.sizeDelta.x, 0f);
        }
    }

    public float Progress()
    {
        return timer / DelayBuild;
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0f, 45f, 0f));
    }
}
