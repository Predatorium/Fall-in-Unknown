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
                building.transform.rotation = transform.rotation;
                building.UI = UI;

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

    public void Rotate()
    {
        transform.Rotate(new Vector3(0f, 45f, 0f));
    }
}
