using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    GameManager gameManager;
    public enum InteractibleType
    {
        Tab,
        Item,
    }
    public InteractibleType type;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.Test();
            if(Input.GetKey(KeyCode.E))
            {

            }
        }
    }
    public void Act()
    {
        if(type == InteractibleType.Tab)
        {

        }
    }
}
