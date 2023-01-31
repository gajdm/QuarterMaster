using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager { get; private set; }


    //FUNCTIONS
    public void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(this);
        }
        else if (Manager != this)
        {
            Destroy(gameObject);
        }
    }
    public void Test()
    {
        Debug.Log("GM");
    }
    public void TooltipToggle(bool value)
    {
        if(value)
        {

        }
        else
        {

        }
    }

}
