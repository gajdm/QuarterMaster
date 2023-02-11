using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManuUI : MonoBehaviour
{
    [SerializeField] private ManuButton button1;
    [SerializeField] private ManuButton button2;
    [SerializeField] private ManuButton button3;
    [SerializeField] private ManuButton button4;

    [SerializeField] private Manufacturer[] manufacturers;

    //VARIABLES
    //
    //FUNCTIONS
    public void Start()
    {
        button1.UpdateButton(manufacturers[0]);
        button2.UpdateButton(manufacturers[1]);
        button3.UpdateButton(manufacturers[2]);
        button4.UpdateButton(manufacturers[3]);
    }
    public void UpdateUI()
    {

    }
    public void OpenManuCard(int number)
    {

    }
}
