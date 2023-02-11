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
    [SerializeField] private ManuCard card;


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
        Manufacturer manufacturer;
        switch (number)
        {
            case 1:
                manufacturer = manufacturers[0];
                break;
            case 2:
                manufacturer = manufacturers[1];
                break;
            case 3:
                manufacturer = manufacturers[2];
                break;
            case 4:
                manufacturer = manufacturers[3];
                break;
            case 5:
                manufacturer = manufacturers[4];
                break;
            case 6:
                manufacturer = manufacturers[5];
                break;
            case 7:
                manufacturer = manufacturers[6];
                break;
            case 8:
                manufacturer = manufacturers[7];
                break;
            default:
                return;
        }
        card.Open(manufacturer);
    }
}
