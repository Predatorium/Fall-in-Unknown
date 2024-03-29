using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecrutUnit : MonoBehaviour
{
    public Transform Destination = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecrutingUnit(string name)
    {
        Unit unit = ResourcesManager.instance.BuyingEntity(name) as Unit;
        if (unit)
        {
            Unit tmpunit = Instantiate(unit, UnitsManager.Instance.Parent);
            ResourcesManager.instance.Purchase(ref unit.Price());
            GameManager.Instance.MyEntity.Add(tmpunit);
            tmpunit.agent.Warp(transform.position - transform.forward * transform.localScale.z);
            tmpunit.SetDestination(Destination.position);
        }
    }
}
