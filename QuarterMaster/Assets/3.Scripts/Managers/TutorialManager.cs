using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NoSuchStudio.UI.Highlight;
using NoSuchStudio.UI.Highlight.Extentions;

public class TutorialManager : MonoBehaviour
{
    [Header("Initial Set-up")]
    public Options optionsSO;
    public Text bodyText;
    public enum Levels
    {
        Living,
        Shop,
        Bellow
    }
    public Levels level;

    //Elements
    [Header("Living Quarters")]

    [SerializeField] private RectTransform tutorialMenu;
    [TextArea(5,10)]
    [SerializeField] private string tutorialString;

    [SerializeField] private RectTransform pauseButton;
    [TextArea(5, 10)]
    [SerializeField] private string pauseString;

    [SerializeField] private Transform player;
    [TextArea(5, 10)]
    [SerializeField] private string playerString;

    [SerializeField] private RectTransform ladders;
    [TextArea(5, 10)]
    [SerializeField] private string laddersString;

    [Header("Shop Quarters")]

    [SerializeField] private RectTransform whatever;
    [TextArea(5, 10)]
    [SerializeField] private string whateverString;

    [Header("The Bellow")]

    //Bool Checks
    [Header("Bool Checks")]
    [SerializeField] private bool hasSeenLadders;

    //First Action Happens Here
    private void Start()
    {
        HighlightUI.defaultOptions = optionsSO;
        switch (level)
        {
            case Levels.Living:
                HighlightUI.defaultOptions.dismissAction = ShowPauseTutorial;
                HighlightUI.ShowForUI(tutorialMenu);
                bodyText.text = tutorialString;
                break;

            case Levels.Shop:
                HighlightUI.defaultOptions.dismissAction = ShowWhatever;
                HighlightUI.ShowForUI(whatever);
                bodyText.text = whateverString;
                break;

            case Levels.Bellow:

            break;

            default:
            break;

        }
        //Assigning the UI to the highlight Canvas so it fades together
        GameObject higlights = GameObject.Find("NoSuchStudio_HighlightCanvas");
        tutorialMenu.SetParent(higlights.transform);
    }

    //Actions 

    //Living Quarters
    private void ShowPauseTutorial()
    {
        HighlightUI.defaultOptions.dismissAction = ShowPlayerTutorial;
        HighlightUI.ShowForUI(pauseButton);
        bodyText.text = pauseString;
    }
    private void ShowPlayerTutorial()
    {
        HighlightUI.defaultOptions.dismissAction = ShowLaddersTutorial;

        ChangePadding(75, 75, 100, 100);

        HighlightUI.ShowFor3DObject(player);
        bodyText.text = playerString;
    }
    private void ShowLaddersTutorial()
    {
        if (!hasSeenLadders)
        {
            ChangePadding(0,0,0,0);

            hasSeenLadders = true;
            HighlightUI.ShowForUI(ladders);
            bodyText.text = laddersString;
        }
        else
        {
            tutorialMenu.gameObject.SetActive(false);
            ladders.gameObject.SetActive(false);
        }
    }

    //Shop Quarters
    private void ShowWhatever()
    {
        HighlightUI.defaultOptions.dismissAction = ShowPlayerTutorial;
        HighlightUI.ShowForUI(pauseButton);
        bodyText.text = pauseString;
    }

    //Utilitty Functions

    private void ChangePadding(int left, int right, int top, int bottom)
    {
        HighlightUI.defaultOptions.padding.right = right;
        HighlightUI.defaultOptions.padding.left = left;
        HighlightUI.defaultOptions.padding.top = top;
        HighlightUI.defaultOptions.padding.bottom = bottom;
    }

    



}
