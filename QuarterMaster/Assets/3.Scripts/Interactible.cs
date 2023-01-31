using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    GameManager gameManager;
    public enum InteractibleType
    {
        Tab,Item,Rack,Crate,Bag,Map
    }
    public InteractibleType type;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            if (Input.GetKey(KeyCode.E)) 
                Act();
        }
    }
    public void Act()
    {
        gameManager = FindObjectOfType<GameManager>();
        switch (type)
        {
            case InteractibleType.Tab:
                break;
            case InteractibleType.Rack:
                break;
            default:
                break;

        }
    }
}
