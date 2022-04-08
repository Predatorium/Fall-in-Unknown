using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIResource : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private Image image = null;
    public ResourceCounter ressource;

    private void Start()
    {
        image.sprite = ressource.resource.sprite;
    }

    private void Update()
    {
        text.text = "+" + ressource.quantity.ToString();
    }
}
