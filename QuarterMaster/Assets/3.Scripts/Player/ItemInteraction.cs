using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Transform pickUpArea;
    [SerializeField] private Transform putDownArea;

    [SerializeField] private bool isHolding;

    [SerializeField] private GameObject currentItem;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject tooltip;
    //ANIMATION
    [SerializeField] private Animator animator;
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
            animator.SetTrigger("PickUp");
            animator.SetBool("Carry", true);
            audioSource.Play();
            uiManager.UpdateItemBar(item);
            tooltip.SetActive(true);

            item.GetComponent<BoxCollider2D>().enabled = false;
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
        audioSource.Play();
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
        } 
    }
}
