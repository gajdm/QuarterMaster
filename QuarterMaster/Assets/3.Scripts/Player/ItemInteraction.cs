using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private bool canInteract;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Transform pickUpArea;
    [SerializeField] private Transform putDownArea;

    [SerializeField] private bool isHolding;

    [SerializeField] private GameObject currentItem;

    [SerializeField] private GameObject tooltip;
    //ANIMATION
    [SerializeField] private Animator animator;
    //Audio
    [SerializeField] private AudioManager audioManager;
    //UPDATE

    public void Update()
    {
        if (isHolding && canInteract)
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
        if (!isHolding && canInteract)
        {
            animator.SetTrigger("PickUp");
            animator.SetBool("Carry", true);
            audioManager.PlaySound("PickUp");
            uiManager.UpdateItemBar(item);
            tooltip.SetActive(true);

            item.GetComponent<BoxCollider2D>().enabled = false;
            item.transform.parent = this.transform;
            item.transform.position = pickUpArea.transform.position;
            currentItem = item;
            isHolding = true;
        }
        else
            return;
    }
    public void PutDown()
    {
        audioManager.PlaySound("PutDown");
        animator.SetBool("Carry", false);
        uiManager.ClearItemBar();
        tooltip.SetActive(false);

        currentItem.GetComponent<BoxCollider2D>().enabled = true;
        currentItem.transform.position = putDownArea.transform.position;
        currentItem.transform.parent = null;
        currentItem = null;
        isHolding = false;
        uiManager.CloseTooltips(false, true);
    }
    public void SetIsHolding(bool value)
    {
           isHolding=value;
        if (!isHolding)
        { 
            uiManager.ClearItemBar();
            animator.SetBool("Carry", false);
            tooltip.SetActive(false);
        } 
    }
    public void SetCanInteract(bool value)
    {canInteract=value;}
}
