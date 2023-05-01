using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private List<Text> orderTexts;
    [SerializeField] private List<Text> amountTexts;
    private void Start()
    {
    }
    public void AddOrder(string text,int fullAmount)
    {
        for (int i = orderTexts.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                orderTexts[i].text = text;
            }
            else
            {
                orderTexts[i].text = orderTexts[i - 1].text;
            }
        }
        for (int i = amountTexts.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                amountTexts[i].text = 0+"/"+fullAmount.ToString();
            }
            else
            {
                amountTexts[i].text = amountTexts[i - 1].text;
            }
        }
    }
    public void UpdateAmount(string code, int current, int full)
    {
        Debug.Log("€");
        for (int i = 0; i <= orderTexts.Count; i++)
        {
            Debug.Log("|");
            if(orderTexts[i].text == code)
            {
                amountTexts[i].text =current.ToString()+"/"+full.ToString();
                break;
            }
        }
    }
}
