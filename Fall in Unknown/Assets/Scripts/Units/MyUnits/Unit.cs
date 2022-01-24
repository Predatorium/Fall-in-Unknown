using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Unit : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!attacker.myTarget)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attacker.range, 1 << LayerMask.NameToLayer("Player"));
            foreach (Enemy enemy in colliders.OfType<Enemy>())
            {
                attacker.myTarget = enemy;
            }
        }
    }

    public override void OnSelect()
    {
        isSelected = true;
    }

    public override void OnUnselect()
    {
        isSelected = false;
    }

    public override void TakeDamages(int damages)
    {
        base.TakeDamages(damages);
    }
}
