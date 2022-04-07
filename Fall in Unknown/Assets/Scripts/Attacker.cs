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
            if (coolDown >= 1f / coolDown_Attack && InReach())
            {
                coolDown = 0f;
                if (attackPrefab)
                {
                    Attack attack = Instantiate(attackPrefab);
                    attack.transform.position = transform.position;
                    attack.target = myTarget;
                    attack.damages = damages;
                }
                else
                {
                    Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, mask);
                    foreach(Collider enemy in colliders)
                    {
                        Entity entity = enemy.GetComponent<Entity>();
                        entity.ChangeHealth(-damages);
                    }
                }
            }
            else if (coolDown < 1f / coolDown_Attack)
            {
                coolDown += Time.deltaTime;
            }
        }
    }

    public bool InReach()
    {
        return Vector3.Distance(transform.position, myTarget.transform.position) <= attackRange &&
            Physics.Raycast(transform.position, myTarget.transform.position - transform.position, attackRange, mask);
    }
}
