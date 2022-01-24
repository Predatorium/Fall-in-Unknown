using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
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
