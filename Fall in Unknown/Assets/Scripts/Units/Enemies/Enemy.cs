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
    }

    public override void ChangeHealth(int damages)
    {
        base.ChangeHealth(damages);
    }
}
