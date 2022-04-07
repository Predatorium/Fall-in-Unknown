using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIResource : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private Image image = null;
    public Resource ressource;

    private void Start()
    {
        image.sprite = ressource.sprite;
    }

    private void Update()
    {
        text.text = "+" + ressource.quantity.ToString();
    }
}
