using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Attack attackPrefab = null;

    [SerializeField] public Entity myTarget = null;

    [SerializeField] private float coolDown_Attack = 0f;
    [SerializeField] private int damages = 1;
    [SerializeField] public float range = 1f;

    private float coolDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget)
        {
            if (coolDown <= 0f && InReach())
            {
                coolDown = coolDown_Attack;
                if (attackPrefab)
                {
                    Attack attack = Instantiate(attackPrefab);
                    attack.transform.position = transform.position;
                    attack.target = myTarget;
                    attack.damages = damages;
                }
                else
                {
                    myTarget.TakeDamages(damages);
                }
            }
        }

        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
    }

    public bool InReach()
    {
        return Vector3.Distance(transform.position, myTarget.transform.position) <= range;
    }
}
