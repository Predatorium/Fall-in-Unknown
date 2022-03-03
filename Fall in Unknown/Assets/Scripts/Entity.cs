using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private List<Ressources> price = null;
    [SerializeField] private float maxLife = 1f;
    public string Name = "";
    public Attacker attacker = null;

    [SerializeField] private UIEntity prefabsUILife = null;
    protected UIEntity UILife = null;

    [SerializeField] private GameObject UISelected = null;

    private float life = 0f;

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
    }

    public virtual void OnUnselect()
    {
        isSelected = false;
        UISelected.SetActive(isSelected);
    }

    public bool Buyable(ref List<Ressources> resources)
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

    public ref List<Ressources> Price()
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
}
