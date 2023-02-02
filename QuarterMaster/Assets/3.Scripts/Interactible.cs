using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    GameManager gameManager;
    UIManager uiManager;
    [SerializeField] private enum InteractibleType
    {
        Tab,Item,Rack,Crate,Bag,Map
    }
    [SerializeField] private InteractibleType type;
    [SerializeField] private string uiName; 

    //FUNCTIONS
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        { 
                Debug.Log("Player near "+type.ToString());
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
    public void Act()
    {
        if(gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        if(uiManager == null)
            uiManager = FindObjectOfType<UIManager>();

        switch (type)
        {
            case InteractibleType.Tab:
                OpenTab(uiName);
                break;
            case InteractibleType.Rack:
                break;
            default:
                break;
        }
    }
    public void OpenTab(string name)
    {
        uiManager.SwitchUI(name, true);
    }
}
