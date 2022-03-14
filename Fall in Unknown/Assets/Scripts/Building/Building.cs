using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Building : Entity
{
    public GameObject UI = null;
    public RecrutUnit recrut = null;

    public float DelayBuild = 0f;

    public Ressources[] Product = null;
    public Ressources[] ContiniousProduct = null;
    private float timeForProduct = 0f;

    [SerializeField] protected GameObject resourcesUI = null;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (Product.Length > 0)
        {
            RessourcesManager.Instance.Sell(ref Product);
        }

        if (ContiniousProduct.Length > 0)
        {
            resourcesUI = Instantiate(RessourcesManager.Instance.prefabsParentUIResource, GameManager.Instance.ParentUI).gameObject;

            foreach (Ressources uI in ContiniousProduct)
            {
                UIResource tmp = Instantiate(RessourcesManager.Instance.prefabsResourceGroup, resourcesUI.transform);
                tmp.text.text = "+" + uI.quantity;
                tmp.image.sprite = RessourcesManager.Instance.ressources.Where(o => o.type == uI.type).First().sprite;
            }
        }
    }

    public override void OnSelect()
    {
        base.OnSelect();
        if (resourcesUI)
            resourcesUI.SetActive(true);
    }

    public override void OnUnselect()
    {
        base.OnUnselect();
        if (resourcesUI)
            resourcesUI.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (ContiniousProduct.Length > 0)
        {
            timeForProduct += Time.deltaTime;

            Vector3 screenPos = GameManager.Instance.cam.WorldToScreenPoint(transform.position) + new Vector3(0, 40);
            resourcesUI.transform.localPosition = new Vector3(screenPos.x - (Screen.width / 2), screenPos.y - (Screen.height / 2), 0f) / GameManager.Instance.canvas.scaleFactor;

            if (timeForProduct >= 60f)
            {
                timeForProduct = 0f;
                RessourcesManager.Instance.Sell(ref ContiniousProduct);
            }
        }
    }

    public override void ChangeHealth(int damages)
    {
        base.ChangeHealth(damages);
    }

    private void OnDestroy()
    {
        Destroy(resourcesUI.gameObject);
    }
}
