using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEntity : MonoBehaviour
{
    [SerializeField] private Image lifeBarre = null;
    [HideInInspector] public Entity owner = null;
    private Coroutine hide = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (owner != null)
        {
            lifeBarre.fillAmount = owner.PourcentageLife();

            Vector3 screenPos = GameManager.Instance.cam.WorldToScreenPoint(owner.transform.position) + new Vector3(0, 40);
            transform.localPosition = new Vector3(screenPos.x - (Screen.width / 2), screenPos.y - (Screen.height / 2), 0f) / GameManager.Instance.canvas.scaleFactor;
        }
        else
            Destroy(gameObject);
    }

    public void resetUI()
    {
        gameObject.SetActive(true);

        if (hide != null)
        {
            StopCoroutine(hide);
        }

        hide = StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        hide = null;
    }
}
