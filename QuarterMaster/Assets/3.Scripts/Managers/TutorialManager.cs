using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NoSuchStudio.UI.Highlight;
using NoSuchStudio.UI.Highlight.Extentions;

using SoulGames.EasyGridBuilderPro;

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

    //Bool Checks
    [Header("Bool Checks")]
    [SerializeField] private bool hasSeenLadders;
    [SerializeField] private bool hasSeenBellowArrow;
    [SerializeField] private bool hasPickedUpBox;
    [SerializeField] private bool hasSeenCodexButton;

    //Elements
    [Header("Living Quarters")]

    [SerializeField] private RectTransform tutorialMenu;
    [SerializeField] private RectTransform ladders;
    [SerializeField] private RectTransform pauseButton;
    [SerializeField] private Transform player;

    [TextArea(5, 10)][SerializeField] private string tutorialString;
    [TextArea(5, 10)][SerializeField] private string pauseString;
    [TextArea(5, 10)][SerializeField] private string playerString;
    [TextArea(5, 10)][SerializeField] private string laddersString;

    [Header("Shop Quarters")]

    [SerializeField] private Transform box;
    [SerializeField] private RectTransform itemBar;
    [SerializeField] private Transform tooltip;
    [SerializeField] private RectTransform bellowArrow;

    [TextArea(5, 10)][SerializeField] private string boxString;
    [TextArea(5, 10)][SerializeField] private string itemBarString;
    [TextArea(5, 10)][SerializeField] private string tooltipString;
    [TextArea(5, 10)][SerializeField] private string bellowArrowString;

    [Header("The Bellow")]

    //tutorial for sorting
    [SerializeField] private RectTransform logsBar;
    [SerializeField] private Transform importPortal;
    [SerializeField] private Transform exportPortal;
    [SerializeField] private RectTransform actionBar;
    [SerializeField] private RectTransform manuButton;
    [SerializeField] private RectTransform orderButton;
    [SerializeField] private RectTransform buildButton;
    //tutorial for economy
    //build button again
    [SerializeField] private RectTransform smallBuildButton;
    [SerializeField] private RectTransform categories;
    [SerializeField] private RectTransform buildings;
    [SerializeField] private Transform blockedArea;
    //tutorial on how to build 
    //tutorial on how to sort
    [SerializeField] private RectTransform codexButton;

    //[SerializeField] private BuildConditionSO conditionRack;
    //[SerializeField] private BuildConditionSO conditionLabelling;
    //[SerializeField] private BuildConditionSO conditionGnome;
    //[SerializeField] private BuildConditionSO conditionConveyor;

    [TextArea(5, 10)][SerializeField] private string sortingSystemString;
    [TextArea(5, 10)][SerializeField] private string logsString;
    [TextArea(5, 10)][SerializeField] private string importString;
    [TextArea(5, 10)][SerializeField] private string exportString;
    [TextArea(5, 10)][SerializeField] private string actionBarString;
    [TextArea(5, 10)][SerializeField] private string manuButtonString;
    [TextArea(5, 10)][SerializeField] private string orderButtonString;
    [TextArea(5, 10)][SerializeField] private string buildButtonString;
    [TextArea(5, 10)][SerializeField] private string economyString;
    [TextArea(5, 10)][SerializeField] private string buildButtonSecondString;
    [TextArea(5, 10)][SerializeField] private string smallBuildButtonString;
    [TextArea(5, 10)][SerializeField] private string categoriesString;
    [TextArea(5, 10)][SerializeField] private string buildingsString;
    [TextArea(5, 10)][SerializeField] private string blockedAreaString;
    [TextArea(5, 10)][SerializeField] private string buildingSystemString;
    [TextArea(5, 10)][SerializeField] private string sortingHowToString;
    [TextArea(5, 10)][SerializeField] private string codexButtonString;

    

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
                HighlightUI.defaultOptions.dismissAction = ShowItemBar;
                HighlightUI.ShowFor3DObject(box);
                bodyText.text = boxString;
                break;

            case Levels.Bellow:
                HighlightUI.defaultOptions.dismissAction = ShowLogsButton;
                HighlightUI.ShowForUI(tutorialMenu);
                bodyText.text = sortingSystemString;
                break;

            default:
            break;

        }
        //Assigning the UI to the highlight Canvas so it fades together
        GameObject higlights = GameObject.Find("NoSuchStudio_HighlightCanvas");
        tutorialMenu.SetParent(higlights.transform);
    }

    //ACTIONS

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
            HighlightUI.Dismiss();
        }
    }

    //Shop Quarters
    private void ShowItemBar()
    {
        if(!hasPickedUpBox)
        {
            HighlightUI.defaultOptions.dismissAction = ShowItemBar;
        }
        else
        {
            ChangePadding(0,200, 600, 0);

            HighlightUI.defaultOptions.dismissAction = ShowTooltip;
            HighlightUI.ShowForUI(itemBar);
            bodyText.text = itemBarString;
        }
    }
    private void ShowTooltip()
    {
        ChangePadding (50, 50, 50, 50);

        HighlightUI.defaultOptions.dismissAction= ShowBellowArrow;
        HighlightUI.ShowFor3DObject(tooltip);
        bodyText.text = tooltipString;
    }
    private void ShowBellowArrow()
    {
        if(!hasSeenBellowArrow)
        {
            hasSeenBellowArrow = true;
            HighlightUI.ShowForUI(bellowArrow);
            bellowArrow.gameObject.SetActive(true);
            bodyText.text = bellowArrowString;
        }
        else
        {
            tutorialMenu.gameObject.SetActive(false);
            bellowArrow.gameObject.SetActive(false);
            HighlightUI.Dismiss();
        }
    }

    //Bellow
    private void ShowLogsButton()
    {
        HighlightUI.defaultOptions.dismissAction = ShowImportPortal;
        HighlightUI.ShowForUI(logsBar);
        bodyText.text = logsString;
    }
    private void ShowImportPortal()
    {
        HighlightUI.defaultOptions.dismissAction = ShowExportPortal;
        HighlightUI.ShowFor3DObject(importPortal);
        bodyText.text = importString;
    }
    private void ShowExportPortal()
    {
        HighlightUI.defaultOptions.dismissAction = ShowActionBar;
        HighlightUI.ShowFor3DObject(exportPortal);
        bodyText.text = exportString;
    }
    private void ShowActionBar()
    {
        HighlightUI.defaultOptions.dismissAction = ShowManuButton;
        HighlightUI.ShowForUI(actionBar);
        bodyText.text = actionBarString;
    }
    private void ShowManuButton()
    {
        HighlightUI.defaultOptions.dismissAction = ShowOrderButton;
        HighlightUI.ShowForUI(manuButton);
        bodyText.text = manuButtonString;
    }
    private void ShowOrderButton()
    {
        HighlightUI.defaultOptions.dismissAction = ShowBuildButton;
        HighlightUI.ShowForUI(orderButton);
        bodyText.text = orderButtonString;
    }
    private void ShowBuildButton()
    {
        HighlightUI.defaultOptions.dismissAction = ShowEconomyTutorial;
        HighlightUI.ShowForUI(buildButton);
        bodyText.text = buildButtonString;
    }
    private void ShowEconomyTutorial()
    { 
        HighlightUI.defaultOptions.dismissAction = ShowBuildButtonSecondTime;
        HighlightUI.ShowForUI(tutorialMenu);
        bodyText.text = economyString;
    }
    private void ShowBuildButtonSecondTime()
    {
        HighlightUI.defaultOptions.dismissAction = ShowSmallBuildButton;
        HighlightUI.ShowForUI(buildButton);
        bodyText.text = buildButtonSecondString;
    }
    private void ShowSmallBuildButton()
    {
        HighlightUI.defaultOptions.dismissAction = ShowCategories;
        HighlightUI.ShowForUI(smallBuildButton);
        bodyText.text = smallBuildButtonString;
    }
    private void ShowCategories()
    {
        HighlightUI.defaultOptions.dismissAction = ShowBuildings;
        HighlightUI.ShowForUI(categories);
        bodyText.text = categoriesString;
    }
    private void ShowBuildings()
    {
        HighlightUI.defaultOptions.dismissAction = ShowBlockedArea;
        HighlightUI.ShowForUI(buildings);
        bodyText.text = buildingsString;
    }
    private void ShowBlockedArea()
    {
        HighlightUI.defaultOptions.dismissAction = ShowBuildTutorial;
        HighlightUI.ShowFor3DObject(blockedArea);
        bodyText.text = blockedAreaString;
    }
    private void ShowBuildTutorial()
    {
        HighlightUI.defaultOptions.dismissAction = ShowSortingTutorial;
        HighlightUI.ShowForUI(tutorialMenu);
        bodyText.text = buildingSystemString;
    }
    private void ShowSortingTutorial()
    {
        HighlightUI.defaultOptions.dismissAction = ShowCodexButton;
        HighlightUI.ShowForUI(tutorialMenu);
        bodyText.text = sortingHowToString;
    }
    private void ShowCodexButton()
    {
        if(!hasSeenCodexButton)
        {
            hasSeenCodexButton = true;
            HighlightUI.defaultOptions.dismissAction = ShowBellowArrow;
            HighlightUI.ShowForUI(codexButton);
            bodyText.text = codexButtonString;
        }
        else
        {
            tutorialMenu.gameObject.SetActive(false);
            HighlightUI.Dismiss();
        }
    }

    //Utilitty Functions

    private void ChangePadding(int left, int right, int top, int bottom)
    {
        HighlightUI.defaultOptions.padding.right = right;
        HighlightUI.defaultOptions.padding.left = left;
        HighlightUI.defaultOptions.padding.top = top;
        HighlightUI.defaultOptions.padding.bottom = bottom;
    }
    public void SetPickedUp(bool value)
    {
        hasPickedUpBox = value;
    }
}
