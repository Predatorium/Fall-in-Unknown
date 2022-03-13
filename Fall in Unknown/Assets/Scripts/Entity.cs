using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private Ressources[] price = null;
    public int maxLife = 1;
    [SerializeField] private Sprite icon = null;
    public string Name = "";
    public Attacker attacker = null;

    [SerializeField] private UIEntity prefabsUILife = null;
    protected UIEntity UILife = null;

    [SerializeField] protected GameObject UISelected = null;
    [SerializeField] private UiInfoEntity prefabsInfo = null;
    private UiInfoEntity myInfo = null;

    public int life { get; protected set; } = 0;

    public bool isSelected = false;

    protected virtual void Awake()
    {
        life = maxLife;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        UILife = Instantiate(prefabsUILife, GameManager.Instance.canvas.transform);
        UILife.owner = this;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void OnSelect()
    {
        isSelected = true;
        UISelected.SetActive(isSelected);

        myInfo = Instantiate(prefabsInfo, GameManager.Instance.InfoEntity);
        myInfo.owner = this;
        //myInfo.iconChange.sprite = icon;
        myInfo.maxLife = maxLife;
    }

    public virtual void OnUnselect()
    {
        isSelected = false;
        UISelected.SetActive(isSelected);

        Destroy(myInfo.gameObject);
    }

    public bool Buyable(ref Ressources[] resources)
    {
        foreach (Ressources resource in price)
        {
            if (resources.Where(r => (r.type == resource.type)).First().quantity < resource.quantity)
            {
                return false;
            }
        }

        return true;
    }

    public ref Ressources[] Price()
    {
        return ref price;
    }

    public float PourcentageLife()
    {
        return (float)life / (float)maxLife;
    }

    public virtual void ChangeHealth(int modifLife)
    {
        life = Mathf.Clamp(life + modifLife, 0, maxLife);

        UILife.resetUI();

        if (life <= 0)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            if (GameManager.Instance.MyEntity.Contains(this))
            {
                GameManager.Instance.MyEntity.Remove(this);
            }
        }
    }
}
