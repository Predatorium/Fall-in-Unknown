using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Attacker attacker = null;
    [SerializeField] private float maxLife = 1f;

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

    public virtual void TakeDamages(int damages)
    {
        life -= damages;
        life = Mathf.Clamp(life, 0, maxLife);
    }
}
