using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager Instance = null;

    private List<Unit> units = new List<Unit>();

    [SerializeField] private Image prefabUI = null;
    private Image UI = null;
    private bool UIisActive = false;

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
        if (units.Count > 0 && UIisActive == false)
        {
            UIisActive = true;
            UI = Instantiate(prefabUI, GameManager.Instance.canvas.transform);
        }
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
            Entity entity = mouseHit.collider.GetComponent<Entity>();
            if (entity)
            {
                foreach (Unit unit in units)
                {
                    if (entity.gameObject != unit.gameObject)
                        unit.attacker.myTarget = entity;
                }
            }

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

        if (UIisActive == true)
        {
            Destroy(UI.gameObject);
            UIisActive = false;
        }
    }

    public void DestroyUnits()
    {
        while (units.Count > 0)
        {
            Unit tmp = units[0];
            units.Remove(units[0]);
            Destroy(tmp.gameObject);
        }
    }
}
