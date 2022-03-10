using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Attack attackPrefab = null;

    [SerializeField] public Entity myTarget = null;

    [SerializeField] private float coolDown_Attack = 0f;
    [SerializeField] private int damages = 1;
    [SerializeField] public float attackRange = 1f;
    [SerializeField] public float detectionRange = 2f;

    private float coolDown = 0f;
    public LayerMask mask;

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
            }
            else if (coolDown > 0)
            {
                coolDown -= Time.deltaTime;
            }
        }
    }

    public bool InReach()
    {
        return Physics.Raycast(transform.position, myTarget.transform.position - transform.position, attackRange, mask);
    }
}
