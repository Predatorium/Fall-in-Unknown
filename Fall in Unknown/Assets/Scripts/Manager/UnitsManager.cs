using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager Instance = null;

    private List<Unit> units = new List<Unit>();

    [SerializeField] private GameObject UI = null;
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

    }

    public void AddUnit(Unit unit)
    {
        if (!units.Contains(unit))
        {
            units.Add(unit);
            unit.OnSelect();
             
            UI.SetActive(true);
        }
    }

    public void MoveUnit()
    {
        Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseray, out RaycastHit mouseHit, Mathf.Infinity))
        {
            Entity entity = mouseHit.collider.GetComponent<Entity>();
            if (entity)
            {
                foreach (Unit unit in units)
                {
                    if (entity.gameObject != unit.gameObject)
                        unit.attacker.myTarget = entity;
                }
            }
            else if (NavMesh.SamplePosition(mouseHit.point, out NavMeshHit Hit, 1f, -1))
            {
                foreach (Unit unit in units)
                {
                    unit.SetDestination(Hit.position);
                    unit.attacker.myTarget = null;
                }
            }
        }
    }

    public void ClearUnits()
    {
        while (units.Count > 0) 
        {
            units[0].OnUnselect();
            units.Remove(units[0]);
        }

        UI.gameObject.SetActive(false);
    }

    public void DestroyUnits()
    {
        while (units.Count > 0)
        {
            Unit tmp = units[0];
            RessourcesManager.Instance.Sell(ref tmp.Price());
            units.Remove(units[0]);
            Destroy(tmp.gameObject);
        }
    }
}
