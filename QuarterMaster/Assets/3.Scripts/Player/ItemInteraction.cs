using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Transform pickUpArea;
    [SerializeField] private Transform putDownArea;
    [SerializeField] private Text debug;
    [SerializeField] private Text debug2;

    [SerializeField] private bool isHolding;
    [SerializeField] private bool isInRange;

    [SerializeField] private GameObject currentItem;

    //UPDATE
    public void Update()
    {
        if (isHolding)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))//Right mouse button
            {
                PutDown();
            }
        }

    }
    //TRIGGER
    public void PickUp(GameObject item)
    {
        if (!isHolding)
        {   
            uiManager.UpdateItemBar(item);
            item.transform.parent = this.transform;
            item.transform.position = pickUpArea.transform.position;
            currentItem = item;
            isHolding = true;
            uiManager.OpenTooltips(false, "", true, "To drop");
        }
        else
            return;
    }
    public void PutDown()
    {
        currentItem.transform.position = putDownArea.transform.position;
        currentItem.transform.parent = null;
        currentItem = null;
        isHolding = false;
        uiManager.CloseTooltips(false, true);
    }
    public void SetIsHolding(bool value)
    {
           isHolding=value;
    }
}
