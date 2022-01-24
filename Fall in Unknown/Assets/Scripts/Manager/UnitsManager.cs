using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class UnitsManager : MonoBehaviour
{
    [SerializeField] private List<Unit> units = new List<Unit>();

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
        if (units.Where(u => (u.gameObject == unit.gameObject)).Count() == 0)
        {
            units.Add(unit);
            unit.OnSelect();
        }
    }

    public void MoveUnit()
    {
        Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseray, out RaycastHit mouseHit, Mathf.Infinity))
        {
            if (NavMesh.SamplePosition(mouseHit.point, out NavMeshHit Hit, 1f, -1))
            {
                foreach (Unit unit in units)
                {
                    unit.SetDestination(Hit.position);
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
    }
}
