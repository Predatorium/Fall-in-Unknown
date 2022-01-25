using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Entity : MonoBehaviour
{
    [SerializeField] private List<Ressources> price = null;
    [SerializeField] private float maxLife = 1f;
    public string Name = "";
    public Attacker attacker = null;

    private float life = 0f;

    public bool isSelected = false;

    protected virtual void Awake()
    {
        attacker = GetComponent<Attacker>();
        life = maxLife;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void OnSelect()
    {
        isSelected = true;
    }

    public virtual void OnUnselect()
    {
        isSelected = false;
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

    public List<Ressources> Price()
    {
        return price;
    }

    public virtual void TakeDamages(int damages)
    {
        life -= damages;
        life = Mathf.Clamp(life, 0, maxLife);
    }
}
