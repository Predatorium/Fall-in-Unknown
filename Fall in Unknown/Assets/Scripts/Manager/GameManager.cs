 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private List<Entity> MyEntity = new List<Entity>();

    [SerializeField] private Image selectionSprite = null;
    [SerializeField] private Camera cam = null;
    public Canvas canvas = null;

    private Vector2 beginMousePos;

    private Entity doubleClick = null;
    private float timeDoubleClick = 0f;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (doubleClick)
        {
            timeDoubleClick += Time.deltaTime;

            if (timeDoubleClick > 0.2f)
            {
                timeDoubleClick = 0f;
                doubleClick = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            selectionSprite.gameObject.SetActive(true);
            selectionSprite.rectTransform.sizeDelta = new Vector2(0f, 0f);
            beginMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            MouseRay();
        }
        if (Input.GetMouseButton(0))
        {
            ResizeImage();
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectionSprite.gameObject.SetActive(false);
            SquareCol();
        }
        if (Input.GetMouseButtonDown(1))
        {
            UnitsManager.Instance.MoveUnit();
        }
    }

    private bool InCamera(Vector3 pos)
    {
        return (cam.WorldToViewportPoint(pos).x > 0f &&
                cam.WorldToViewportPoint(pos).x < 1f &&
                cam.WorldToViewportPoint(pos).y > 0f &&
                cam.WorldToViewportPoint(pos).y < 1f);
    }

    private void DoubleClick(ref Entity selectable)
    {
        Entity cur = selectable as Entity;
        if (doubleClick.gameObject == cur.gameObject)
        {
            foreach (Entity current in FindObjectsOfType<Entity>())
            {
                if (current != null && current.GetType() == doubleClick.GetType())
                {
                    if (InCamera(current.transform.position))
                    {
                        Entity select = current as Entity;
                        ChooseSelect(select);
                        select.OnSelect();
                    }
                }
            }
            
        }
        else
        {
            ClearSelect();
            selectable.OnSelect();
            ChooseSelect(selectable);
            doubleClick = null;
        }
    }

    private void MouseRay()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Ray mouseray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseray, out RaycastHit mouseHit, Mathf.Infinity))
            {
                var selectable = mouseHit.collider.GetComponent<Entity>();
                if (selectable != null)
                {
                    if (doubleClick)
                    {
                        DoubleClick(ref selectable);
                    }
                    else
                    {
                        doubleClick = selectable;
                        timeDoubleClick = 0f;
                        ClearSelect();
                        ChooseSelect(selectable);
                    }
                }
                else
                {
                    ClearSelect();
                }
            }
        }
    }

    private void ResizeImage()
    {
        Vector2 diag = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - beginMousePos;
        Vector2 center = beginMousePos + diag / 2f;

        selectionSprite.rectTransform.sizeDelta = new Vector2(Mathf.Abs(diag.x / Screen.width * 1920), Mathf.Abs(diag.y / Screen.height * 1080));
        selectionSprite.rectTransform.position = center;
    }

    private bool TrueContains(Rect rect, Vector3 odjetPos, Vector3 posSelect)
    {
        return (odjetPos.x < (posSelect.x + rect.xMax) / Screen.width * 1920f
                && odjetPos.x > (posSelect.x + rect.xMin) / Screen.width * 1920f
                && odjetPos.y < (posSelect.y + rect.yMax) / Screen.height * 1080f
                && odjetPos.y > (posSelect.y + rect.yMin) / Screen.height * 1080f);
    }

    private void SquareCol()
    {
        List<Entity> sel = MyEntity.Where(e => (e as Unit)).ToList();

        foreach (Entity select in sel)
        {
            Vector3 objet = cam.WorldToScreenPoint(select.transform.position);
            objet = new Vector3(objet.x / Screen.width * 1920f, objet.y / Screen.height * 1080f);
            Rect rect = selectionSprite.rectTransform.rect;
            Vector3 pos = selectionSprite.rectTransform.position;

            if (TrueContains(rect, objet, pos))
            {
                ChooseSelect(select);
            }
        }
    }

    private void ChooseSelect(Entity entity)
    {
        if (MyEntity.Where(e => (e.gameObject == entity.gameObject)).Count() > 0)
        {
            Unit unit = entity as Unit;
            if (unit != null)
            {
                UnitsManager.Instance.AddUnit(unit);
            }
            else
            {
                BuildingManager.Instance.AddBuilding(entity as Building);
            }
        }
        else
        {
            entity.OnSelect();
        }
    }

    private void ClearSelect()
    {
        UnitsManager.Instance.ClearUnits();
        BuildingManager.Instance.ClearBuildings();
    }
}
