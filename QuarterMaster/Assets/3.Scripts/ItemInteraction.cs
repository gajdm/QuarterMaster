using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private Transform pickUpArea;
    [SerializeField] private Transform putDownArea;

    [SerializeField] private CanvasGroup tooltipE;
    [SerializeField] private CanvasGroup toolTipQ;

    [SerializeField] private bool isHolding;
    [SerializeField] private GameObject currentItem;
    public void Update()
    {
        if (isHolding)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                PickUp(currentItem);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            tooltipE.alpha = 1;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (Input.GetKey(KeyCode.E))
            {
                PickUp(collision.gameObject);
            } 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
            tooltipE.alpha = 0;
    }
    public void PickUp(GameObject item)
    {
        if (isHolding)
        {
            currentItem.transform.position = putDownArea.transform.position;
            currentItem.transform.parent = null;
            currentItem = null;
            isHolding = false;
            toolTipQ.alpha = 0;
        }
        else
        {
            item.transform.parent = pickUpArea.transform;
            item.transform.position = pickUpArea.transform.position;
            currentItem = item;
            isHolding = true;
            toolTipQ.alpha = 1;
        }

    }
}
