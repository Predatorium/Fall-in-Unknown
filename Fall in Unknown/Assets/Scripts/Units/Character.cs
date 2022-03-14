using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Entity
{
    public UnityEngine.AI.NavMeshAgent agent = null;

    protected enum State
    {

    }

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

        if (Vector3.Distance(transform.position, agent.destination) < 1f && !attacker.myTarget)
        {
            agent.ResetPath();
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

    protected virtual void Attacker()
    {

    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public override void ChangeHealth(int damages)
    {
        base.ChangeHealth(damages);
    }
}
