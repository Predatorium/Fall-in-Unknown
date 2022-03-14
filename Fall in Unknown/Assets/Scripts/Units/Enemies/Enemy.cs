using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Character
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

        if (UISelected.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnUnselect();
            }
        }
    }

    public override void OnSelect()
    {
        base.OnSelect();
    }

    public override void OnUnselect()
    {
        base.OnUnselect();
    }

    protected override void Attacker()
    {
        base.Attacker();

        if (!attacker.myTarget)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attacker.detectionRange, attacker.mask);
            foreach (Collider enemy in colliders)
            {
                attacker.myTarget = enemy.GetComponent<Entity>();
            }
        }
        else
        {
            if (!attacker.InReach())
            {
                SetDestination(attacker.myTarget.transform.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
    }

    public override void ChangeHealth(int damages)
    {
        base.ChangeHealth(damages);
    }
}
