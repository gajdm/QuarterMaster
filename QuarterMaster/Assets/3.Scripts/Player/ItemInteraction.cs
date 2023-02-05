using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private Transform pickUpArea;
    [SerializeField] private Transform putDownArea;
    [SerializeField] private Text debug;
    [SerializeField] private Text debug2;

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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Crate")
        {
            if (Input.GetKey(KeyCode.E))
            {
                PickUp(collision.gameObject);
            } 
        }
    }
    public void PickUp(GameObject item)
    {
        if (isHolding)
        {
            currentItem.transform.position = putDownArea.transform.position;
            currentItem.transform.parent = null;
            currentItem = null;
            isHolding = false;
        }
        else
        {
            item.transform.parent = this.transform;
            item.transform.position = pickUpArea.transform.position;
            currentItem = item;
            isHolding = true;
        }

    }
    public void SetIsHolding(bool value)
    {
           isHolding=value;
    }
}
