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
    [Header("Highlight Options")]
    [SerializeField] private bool hasHighligth;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject tooltip;

    [SerializeField] private string uiName;

    [Header("System debug")]
    [SerializeField] private bool isInRange;
    [SerializeField] private bool mouseOver;
    [SerializeField] private bool colliding;

    [SerializeField] private GameObject player;
    [SerializeField] private ItemInteraction playerBrain;
    [SerializeField] private Transform itemPlacement;
    //Rack variables
    [SerializeField] private Rack rack;

    //FUNCTIONS
    //UPDATES
    public void Start()
    {
        playerBrain = FindObjectOfType<ItemInteraction>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if ((isInRange || colliding) && mouseOver)
            {
                Act(player);
            }
        }
    }
    //MOUSE
    public void OnMouseEnter()
    {
        mouseOver = true;
    }
    public void OnMouseExit()
    {
        mouseOver = false;
    }
    public void OnMouseOver()
    {
        mouseOver = true ;
    }

    //TRIGGERS

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            isInRange = true;
            if(hasHighligth)
            { animator.SetBool("On", true);
                tooltip.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInRange = false;
            if (hasHighligth)
            { animator.SetBool("On", false);
                tooltip.SetActive(false);
            }
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

    private void OnEnable()
    {
        player = FindObjectOfType<ItemInteraction>().gameObject;
        playerBrain = FindObjectOfType<ItemInteraction>();
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        if(hasHighligth)
        {   animator = GetComponentInChildren<Animator>();
            tooltip.SetActive(false);
        }
    }
    public void Act(GameObject player)
    {
        switch (type)
        {
            case InteractibleType.Tab:
                OpenTab(uiName);
                break;
            case InteractibleType.Rack:
                rack = GetComponentInParent<Rack>();
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
                    newGO.transform.parent = itemPlacement;
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
