using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] public Entity target;
    [SerializeField] public float speed = 1f;
    [SerializeField] public int damages = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = target.transform.position - transform.position;
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            target.TakeDamages(damages);
            Destroy(gameObject);
        }
    }
}
