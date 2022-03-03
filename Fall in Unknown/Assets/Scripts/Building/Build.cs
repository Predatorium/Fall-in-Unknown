using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [HideInInspector] public float DelayConstruction = 0f;
    [HideInInspector] public Building prefabsBuilding = null;

    [SerializeField] private Collider collider = null;
    [SerializeField] private LayerMask mask;

    private float timer = 0f;
    public GameObject UI = null;

    public bool IsPlace = false;
    public bool IsPlaceable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlace)
        {
            timer += Time.deltaTime;

            if (timer >= DelayConstruction)
            {
                Building building = Instantiate(prefabsBuilding);
                GameManager.Instance.MyEntity.Add(building);
                building.transform.position = transform.position;
                building.UI = UI;

                Destroy(gameObject);
            }
        }
        else
        {   
            IsPlaceable = true;
            Collider[] colliders = Physics.OverlapBox(transform.position, collider.bounds.extents, transform.rotation, ~(mask));
            if (colliders.Length > 0)
            {
                IsPlaceable = false;
            }
        }
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0f, 90f, 0f));
    }
}
