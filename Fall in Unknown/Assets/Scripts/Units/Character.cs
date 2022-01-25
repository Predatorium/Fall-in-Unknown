using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent = null;

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

    }

    public override void OnSelect()
    {
        base.OnSelect();
    }

    public override void OnUnselect()
    {
        base.OnUnselect();
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public override void TakeDamages(int damages)
    {
        base.TakeDamages(damages);
    }
}
