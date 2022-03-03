using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [HideInInspector] public int damages = 1;
    [HideInInspector] public Entity target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            transform.forward = target.transform.position - transform.position;
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            target.ChangeHealth(-damages);
            Destroy(gameObject);
        }
    }
}
