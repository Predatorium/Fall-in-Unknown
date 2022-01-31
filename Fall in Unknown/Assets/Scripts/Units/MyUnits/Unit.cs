using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Unit : Character
{
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (attacker)
            Attacker();
    }

    public override void OnSelect()
    {
        base.OnSelect();
    }

    public override void OnUnselect()
    {
        base.OnUnselect();
    }

    private void Attacker()
    {
        if (!attacker.myTarget)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attacker.range, 1 << LayerMask.NameToLayer("Player"));
            foreach (Enemy enemy in colliders.OfType<Enemy>())
            {
                attacker.myTarget = enemy;
            }
        }
        else
        {
            if (!attacker.InReach())
            {
                SetDestination(attacker.myTarget.transform.position);
            }
        }
    }

    public override void TakeDamages(int damages)
    {
        base.TakeDamages(damages);
    }
}
