using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEntity : MonoBehaviour
{
    [SerializeField] private Image lifeBarre = null;
    [HideInInspector] public Entity owner = null;
    private float timer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (owner != null)
            lifeBarre.fillAmount = owner.PourcentageLife();
        else
            Destroy(gameObject);

        if (timer <= 0f)
        {
            gameObject.SetActive(false);
            timer = 5f;
        }
    }
    public void resetUI()
    {
        gameObject.SetActive(true);
        timer = 5f;
    }

}
