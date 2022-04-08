using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEntity : MonoBehaviour
{
    [SerializeField] private Button button = null;
    [SerializeField] private Entity prefabsEntity = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.interactable)
        {
            if (!prefabsEntity.Buyable(ref ResourcesManager.instance.resources))
                button.interactable = false;
        }
        else
        {
            if (prefabsEntity.Buyable(ref ResourcesManager.instance.resources))
                button.interactable = true;
        }
    }

    public void CallBack()
    {
        if (prefabsEntity as Unit)
            BuildingManager.Instance.RecrutUnit(prefabsEntity.name);
        else if (prefabsEntity as Building)
            BuildingManager.Instance.ConstructBuilding(prefabsEntity.name);
    }
}
