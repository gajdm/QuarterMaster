using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodexSystem : MonoBehaviour
{
    public List<Button> buttonList;
    public List<Codex> codexList;
    public Codex currentCodex;
    public Codex nextCodex;
    public Codex previousCodex;

    public Image visualImage;
    public int codexLoc;

    public Text header;
    public Text body;
    public Text nextCodexText;
    public Text previousCodexText;
    private void Start()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            Text textOfButton = buttonList[i].GetComponentInChildren<Text>();
            textOfButton.text = codexList[i].GetHeader();
        }
        ButtonPress(true);
    }
    public void UpdateUI()
    {
        visualImage.sprite = currentCodex.GetImage();
        header.text = currentCodex.GetHeader();
        body.text = currentCodex.GetMainText();
        nextCodexText.text = "Next to: "+nextCodex.GetHeader();
        previousCodexText.text = "Back to: "+previousCodex.GetHeader();
    }
    public void ButtonPress(bool next)
    {
        if(next)
        {
            codexLoc++;
            if (codexLoc>codexList.Count-1)
            {
                codexLoc = 0;
            }
        }
        else
        {   
            codexLoc--;
            if (codexLoc <0)
            {
                codexLoc = codexList.Count - 1;
            }
        }

        if(codexLoc == 0)
        {
            nextCodex = codexList[codexLoc + 1];
            previousCodex = codexList[codexList.Count-1];
        }
        else if(codexLoc == codexList.Count-1)
        {
            nextCodex = codexList[0];
            previousCodex = codexList[codexLoc - 1];
        }
        else
        {
            nextCodex = codexList[codexLoc + 1];
            previousCodex = codexList[codexLoc - 1];
        }

        currentCodex = codexList[codexLoc];
        UpdateUI();
    }
    public void JumpButton(int number)
    {
        codexLoc = number;
        if (codexLoc == 0)
        {
            nextCodex = codexList[codexLoc + 1];
            previousCodex = codexList[codexList.Count - 1];
        }
        else if (codexLoc == codexList.Count - 1)
        {
            nextCodex = codexList[0];
            previousCodex = codexList[codexLoc - 1];
        }
        else
        {
            nextCodex = codexList[codexLoc + 1];
            previousCodex = codexList[codexLoc - 1];
        }
        currentCodex =codexList[codexLoc];
        UpdateUI();
    }
}
