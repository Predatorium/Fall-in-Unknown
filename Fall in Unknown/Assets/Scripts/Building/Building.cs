using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : Entity
{
    public GameObject UI = null;
    public RecrutUnit recrut = null;

    public float DelayBuild = 0f;

    [SerializeField] private Ressources[] Product = null;
    [SerializeField] private Ressources[] ContiniousProduct = null;
    private float timeForProduct = 0f;

    protected override void Awake()
    {
        base.Awake();
        recrut = GetComponent<RecrutUnit>();
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
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (ContiniousProduct.Length > 0)
        {
            timeForProduct += Time.deltaTime;

            if (timeForProduct >= 60f)
            {
                timeForProduct = 0f;
                RessourcesManager.Instance.Sell(ref Product);
            }
        }
    }

    public override void ChangeHealth(int damages)
    {
        base.ChangeHealth(damages);
    }
}
