using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogsSystem : MonoBehaviour
{
    public AudioManager audioManager;
    [SerializeField] private Text[] texts;
    public void AddLog(string text)
    {
        audioManager.PlaySound("Event");
        
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
