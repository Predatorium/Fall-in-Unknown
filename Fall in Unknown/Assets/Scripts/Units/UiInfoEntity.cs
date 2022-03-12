using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInfoEntity : MonoBehaviour
{
    [SerializeField] private Image lifeBarre = null;
    [SerializeField] private TextMeshProUGUI textLife = null;
    public Image iconChange = null;
    [HideInInspector] public Entity owner = null;
    [HideInInspector] public int maxLife = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (owner)
        {
            lifeBarre.fillAmount = owner.PourcentageLife();
            textLife.text = owner.life + " / " + maxLife;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
