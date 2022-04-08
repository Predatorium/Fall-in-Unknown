using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : Entity
{
    public NavMeshAgent agent = null;
    [SerializeField] private LayerMask mask;

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
        {
            Attacker();

            if (Vector3.Distance(transform.position, agent.destination) < 1f && !attacker.myTarget)
            {
                agent.ResetPath();
            }
        }

        if (Physics.OverlapSphere(agent.destination, 0.1f, mask).Length > 0 && new Vector3(agent.destination.x, transform.position.y, agent.destination.z) != transform.position)
        {
            Vector3 pos = agent.destination + (transform.position - agent.destination).normalized;
            if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 15f, -1))
                SetDestination(hit.position);
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
