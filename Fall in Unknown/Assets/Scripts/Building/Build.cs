using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        base.Awake();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (IsPlace)
        {
            timer += Time.deltaTime;

            if (timer >= DelayBuild)
            {
                Building building = Instantiate(prefabsBuilding);
                GameManager.Instance.MyEntity.Add(building);
                building.transform.position = transform.position;
                building.transform.rotation = transform.rotation;
                building.UI = transfertUI;

                Destroy(gameObject);
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
        base.Start();
        gameObject.layer = LayerMask.NameToLayer("Player");

        UIBuild tmp = Instantiate(prefabsUIBuild, GameManager.Instance.canvas.transform);
        tmp.owner = this;
    }

    public float Progress()
    {
        return timer / DelayBuild;
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0f, 45f, 0f));
    }

    public override void ChangeHealth(int damages)
    {
        base.ChangeHealth(damages);
    }
}
