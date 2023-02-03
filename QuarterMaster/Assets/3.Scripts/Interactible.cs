using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    GameManager gameManager;
    UIManager uiManager;
    [SerializeField] private enum InteractibleType
    {
        Tab, Item, Rack, Crate, Bag, Map
    }
    [SerializeField] private InteractibleType type;
    [SerializeField] private string uiName;

    //Tooltip variables
    [Header("Tooltips")]

    [SerializeField] private bool tooltipE;
    [SerializeField] private string eString;

    [SerializeField] private bool tooltipQ;
    [SerializeField] private string qString;
    //Rack variables
    [SerializeField] private Rack rack;
    

    //FUNCTIONS
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(uiManager == null) uiManager = FindObjectOfType<UIManager>();
        
        if (collision.gameObject.tag == "Player")
        {
            if(tooltipE || tooltipQ) uiManager.OpenTooltips(tooltipE,eString,tooltipQ,qString);   
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Player pressed E near " + type.ToString());
                Act();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        uiManager.CloseTooltips();
    }
    public void Act()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();

        switch (type)
        {
            case InteractibleType.Tab:
                OpenTab(uiName);
                break;
            case InteractibleType.Rack:
                rack = GetComponent<Rack>();
                OpenTab(uiName);
                break;
            default:
                break;
        }
    }
    public void OpenTab(string name)
    {
        uiManager.SwitchUI(name, true);
        if(type == InteractibleType.Rack)uiManager.UpdateRackUI(rack);
    }
}
