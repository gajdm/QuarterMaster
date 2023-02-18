using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    GameManager gameManager;
    UIManager uiManager;
    [SerializeField] private enum InteractibleType
    {
        Tab, Item, Rack, Crate, Bag, Map, Label, Export
    }
    [SerializeField] private InteractibleType type;
    [SerializeField] private string uiName;

    [SerializeField] private bool isInRange;
    [SerializeField] private bool mouseOver;
    [SerializeField] private bool colliding;

    [SerializeField] private GameObject player;
    [SerializeField] private ItemInteraction playerBrain;


    //Tooltip variables
    [Header("Tooltips")]

    [SerializeField] private bool tooltipE;
    [SerializeField] private string eString;

    [SerializeField] private bool tooltipQ;
    [SerializeField] private string qString;
    //Rack variables
    [SerializeField] private Rack rack;
    //Mouse variables
    public Texture2D mouse;

    //FUNCTIONS
    //UPDATES
    public void Start()
    {
        playerBrain = FindObjectOfType<ItemInteraction>();
    }
    public void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.Mouse0) && mouseOver) || (colliding && Input.GetKey(KeyCode.Space)))
        {
            Act(player);
        }
    }
    //MOUSE
    public void OnMouseEnter()
    {
        mouseOver = true;
        //if (isInRange)
        //{

        //    if (uiManager == null) uiManager = FindObjectOfType<UIManager>();
        //    uiManager.OpenTooltips(tooltipE,eString,tooltipQ,qString);
        //    if (tooltipE || tooltipQ) uiManager.OpenTooltips(tooltipE, eString, tooltipQ, qString);
        //    if (playerBrain == null) playerBrain = FindObjectOfType<ItemInteraction>();
        //}
    }
    public void OnMouseExit()
    {
        mouseOver = false;
        //if (uiManager == null) uiManager = FindObjectOfType<UIManager>();
        //uiManager.CloseTooltips(true,false);
    }

    //TRIGGERS

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            colliding = true;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colliding = false;
        }
    }
    //FUNCTIONS
    public void Act(GameObject player)
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
            case InteractibleType.Label:
                CheckForItem("Crate", player);
                break;
                case InteractibleType.Crate:
                playerBrain.PickUp(this.gameObject);
                break;
            case InteractibleType.Item:
                playerBrain.PickUp(this.gameObject);
                break;
            case InteractibleType.Bag:
                playerBrain.PickUp(this.gameObject);
                break;
            case InteractibleType.Export:
                CheckForItem("Bag", player);
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
    public void CheckForItem(string tag, GameObject player)
    {
       for(int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).tag == tag)
            {
                if(tag == "Crate")
                {
                    GameObject newGO = player.transform.GetChild(i).gameObject;
                    this.gameObject.GetComponent<LabelStation>().GetNewCrate(newGO.GetComponent<Crate>());
                    newGO.GetComponentInParent<ItemInteraction>().SetIsHolding(false);
                    newGO.transform.parent = this.transform;
                    newGO.GetComponent<BoxCollider2D>().enabled = false;
                    newGO.transform.position = this.transform.position;
                }
                else if(tag == "Bag")
                {
                    Bag holdBag = player.GetComponentInChildren<Bag>();
                    BuyerManager buyerManager = FindObjectOfType<BuyerManager>();
                    buyerManager.FinishOrder(holdBag.GetOrder());
                    Destroy(holdBag.gameObject);
                    ItemInteraction playerMind = FindObjectOfType<ItemInteraction>();
                    playerMind.SetIsHolding(false);
                }
            }
        }
    }
}
