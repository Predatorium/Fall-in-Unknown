using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuild : MonoBehaviour
{
    [SerializeField] private Image progressBarre = null;
    [HideInInspector] public Build owner = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (owner != null)
        {
            progressBarre.fillAmount = owner.Progress();

            Vector3 screenPos = GameManager.Instance.cam.WorldToScreenPoint(owner.transform.position) + new Vector3(0, 60);
            transform.localPosition = new Vector3(screenPos.x - (Screen.width / 2), screenPos.y - (Screen.height / 2), 0f) / GameManager.Instance.canvas.scaleFactor;
        }
        else
            Destroy(gameObject);
    }
}
