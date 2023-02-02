using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManufacturerManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Manufacturer[] manufacturerList;
    public void UpdateManuLevel(int manuNumber)//Further development
    {

    }
    public void UpdateManuPrice(int manuNumber)//Further development
    {

    }
    public void UpdateManuIcon(int manuNumber)//Further development
    {

    }
    public void UpdateManuAvailability(int manuNumber)
    {
        manufacturerList[manuNumber].available = true;
    }


}
