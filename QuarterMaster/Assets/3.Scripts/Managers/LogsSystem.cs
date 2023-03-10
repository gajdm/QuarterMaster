using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogsSystem : MonoBehaviour
{
    [SerializeField] private Text[] texts;
    public void AddLog(string text)
    {
        //texts[4].text = texts[3].text;
        //texts[3].text = texts[2].text;
        //texts[2].text = texts[1].text;
        //texts[1].text = texts[0].text;
        //texts[0].text = text;
        
        for(int i = texts.Length-1; i >= 0; i--)
        {
            if (i == 0)
            {
                texts[i].text = text;
            }
            else
            {
                texts[i].text = texts[i - 1].text;
            }           
        }
    }
}
