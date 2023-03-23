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
    public AudioManager audioManager;
    public string soundName;
    public float speedOfTyping;
    public AudioSource audioSource;
    //Bool Checks
    [Header("Bool Checks")]

    //Living and Shop quarters
    [SerializeField] private bool hasSeenLadders;
    [SerializeField] private bool hasSeenBellowArrow;
    [SerializeField] private bool hasPickedUpBox;
    [SerializeField] private bool hasSeenCodexButton;

    //Typewriter
    [SerializeField] private bool typeWriterComplete;
    [SerializeField] private bool typeWriterSkip;

    //Bellow button checks
    [SerializeField] private bool seenLogs;
    [SerializeField] private bool seenAction;

    //Elements
    [Header("Living Quarters")]

    [SerializeField] private RectTransform tutorialMenu;
    [SerializeField] private RectTransform ladders;
    [SerializeField] private RectTransform pauseRect;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject invisibleWall;

    [TextArea(5, 10)][SerializeField] private string tutorialString;
    [TextArea(5, 10)][SerializeField] private string pauseString;
    [TextArea(5, 10)][SerializeField] private string playerString;
    [TextArea(5, 10)][SerializeField] private string laddersString;

    [Header("Shop Quarters")]

    [SerializeField] private Transform box;
    [SerializeField] private RectTransform itemBar;
    [SerializeField] private Transform tooltip;
    [SerializeField] private RectTransform bellowArrow;

    [SerializeField] private PlayerMovement playerMovement;

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
    [SerializeField] private RectTransform money;
    //build button again
    [SerializeField] private RectTransform smallBuildButton;
    [SerializeField] private RectTransform categories;
    [SerializeField] private RectTransform buildings;
    [SerializeField] private Transform blockedArea;
    //tutorial on how to build 
    //tutorial on how to sort
    [SerializeField] private RectTransform codexButton;
    [Space(5)]
    [SerializeField] private Animator logsAnimator;
    [SerializeField] private Animator actionAnimator;

    [SerializeField] private Button manuButtonButton;
    [SerializeField] private Button orderButtonButton;
    [SerializeField] private Button buildButtonButton;
    [SerializeField] private Button logsButton;
    [SerializeField] private Button actionButton;


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
                playerMovement.SetCanMove(false);
                ShowTheItem();
                break;

            case Levels.Bellow:
                HighlightUI.defaultOptions.dismissAction = ShowLogsButton;
                HighlightUI.ShowForUI(tutorialMenu);
                bodyText.text = sortingSystemString;
                break;

            default:
                break;

        }
        AssignTutorialMenu();
    }

    //ACTIONS
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (typeWriterComplete)
            {
                HighlightUI.Dismiss(true);
            }
            else
            {
                typeWriterSkip = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            TutorialSkip();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(HighlightUI.defaultOptions.dismissAction.Method);
        }
    }
    //Living Quarters
    private void ShowPauseTutorial()
    {
        audioManager.PlaySound(soundName);
        HighlightUI.defaultOptions.dismissAction = ShowPlayerTutorial;
        HighlightUI.ShowForUI(pauseRect);
        bodyText.text = pauseString;
        StopAllCoroutines();
        StartCoroutine(Typewriter(pauseString));
    }
    private void ShowPlayerTutorial()
    {
        audioManager.PlaySound(soundName);
        HighlightUI.defaultOptions.dismissAction = ShowLaddersTutorial;

        ChangePadding(75, 75, 100, 100);

        HighlightUI.ShowFor3DObject(player);
        StopAllCoroutines();
        StartCoroutine(Typewriter(playerString));
    }
    private void ShowLaddersTutorial()
    {
        if (!hasSeenLadders)
        {
            audioManager.PlaySound(soundName);
            ChangePadding(0, 0, 0, 0);

            hasSeenLadders = true;
            HighlightUI.ShowForUI(ladders);
            bodyText.text = laddersString;
            StopAllCoroutines();
            StartCoroutine(Typewriter(laddersString));
        }
        else
        {
            audioManager.PlaySound(soundName);
            tutorialMenu.gameObject.SetActive(false);
            ladders.gameObject.SetActive(false);
            HighlightUI.Dismiss();
            pauseButton.interactable = true;
            Destroy(invisibleWall);
        }
    }

    //Shop Quarters
    private void ShowTheItem()
    {
        HighlightUI.defaultOptions.dismissAction = ShowItemBar;
        HighlightUI.ShowFor3DObject(box);
        StopAllCoroutines();
        StartCoroutine(Typewriter(boxString));
    }
    private void ShowItemBar()
    {
        if (!hasPickedUpBox)
        {
            HighlightUI.ShowFor3DObject(box);
            HighlightUI.defaultOptions.dismissAction = ShowTheItem;

            StopAllCoroutines();
            StartCoroutine(Typewriter(boxString));
        }
        else
        {
            ChangePadding(0, 200, 400, 0);

            HighlightUI.defaultOptions.dismissAction = ShowTooltip;
            HighlightUI.ShowForUI(itemBar);
            bodyText.text = itemBarString;

            StopAllCoroutines();
            StartCoroutine(Typewriter(itemBarString));
        }
    }
    private void ShowTooltip()
    {
        ChangePadding(50, 50, 50, 50);

        HighlightUI.defaultOptions.dismissAction = ShowBellowArrow;
        HighlightUI.ShowFor3DObject(tooltip);

        StopAllCoroutines();
        StartCoroutine(Typewriter(tooltipString));
    }
    private void ShowBellowArrow()
    {
        if (!hasSeenBellowArrow)
        {
            hasSeenBellowArrow = true;
            if (bellowArrow != null)
            {
                HighlightUI.ShowForUI(bellowArrow);

                bellowArrow.gameObject.SetActive(true);
            }
            StopAllCoroutines();
            StartCoroutine(Typewriter(bellowArrowString));
        }
        else
        {
            playerMovement.SetCanMove(true);
            audioManager.PlaySound(soundName);
            tutorialMenu.gameObject.SetActive(false);
            bellowArrow.gameObject.SetActive(false);
            HighlightUI.Dismiss();
        }
    }

    //Bellow
    private void ShowLogsButton()
    {
        logsAnimator.SetTrigger("Action");
        HighlightUI.defaultOptions.dismissAction = ShowImportPortal;
        HighlightUI.ShowForUI(logsBar);

        StopAllCoroutines();
        StartCoroutine(Typewriter(logsString));
    }
    private void ShowImportPortal()
    {
        logsAnimator.SetTrigger("Action");

        ChangePadding(100, 100, 100, 100);
        audioManager.PlaySound(soundName);
        HighlightUI.defaultOptions.dismissAction = ShowExportPortal;
        HighlightUI.ShowFor3DObject(importPortal);

        StopAllCoroutines();
        StartCoroutine(Typewriter(importString));
    }
    private void ShowExportPortal()
    {
        HighlightUI.defaultOptions.dismissAction = ShowActionBar;
        HighlightUI.ShowFor3DObject(exportPortal);

        StopAllCoroutines();
        StartCoroutine(Typewriter(exportString));
    }
    private void ShowActionBar()
    {
        actionAnimator.SetTrigger("Action");
        ChangePadding(0, 0, 0, 0);
        HighlightUI.defaultOptions.dismissAction = ShowManuButton;
        HighlightUI.ShowForUI(actionBar);

        StopAllCoroutines();
        StartCoroutine(Typewriter(actionBarString));
    }
    private void ShowManuButton()
    {
        HighlightUI.defaultOptions.dismissAction = ShowOrderButton;
        HighlightUI.ShowForUI(manuButton);

        StopAllCoroutines();
        StartCoroutine(Typewriter(manuButtonString));
    }
    private void ShowOrderButton()
    {
        //HighlightUI.defaultOptions.dismissAction = ShowBuildButton;
        HighlightUI.defaultOptions.dismissAction = Dismiss;
        HighlightUI.ShowForUI(orderButton);

        StopAllCoroutines();
        StartCoroutine(Typewriter(orderButtonString));
    }
    private void ShowBuildButton()
    {
        HighlightUI.defaultOptions.dismissAction = ShowEconomyTutorial;
        HighlightUI.ShowForUI(buildButton);
        StopAllCoroutines();
        StartCoroutine(Typewriter(buildButtonString));
    }
    private void ShowEconomyTutorial()
    {
        HighlightUI.defaultOptions.dismissAction = ShowBuildButtonSecondTime;
        HighlightUI.ShowForUI(money);
        StopAllCoroutines();
        StartCoroutine(Typewriter(economyString));
    }
    private void ShowBuildButtonSecondTime()
    {
        HighlightUI.defaultOptions.dismissAction = ShowSmallBuildButton;
        HighlightUI.ShowForUI(buildButton);
        bodyText.text = buildButtonSecondString;
        StopAllCoroutines();
        StartCoroutine(Typewriter(buildButtonSecondString));
    }
    private void ShowSmallBuildButton()
    {
        HighlightUI.defaultOptions.dismissAction = ShowCategories;
        HighlightUI.ShowForUI(smallBuildButton);
        StopAllCoroutines();
        StartCoroutine(Typewriter(smallBuildButtonString));
    }
    private void ShowCategories()
    {
        HighlightUI.defaultOptions.dismissAction = ShowBuildings;
        HighlightUI.ShowForUI(categories);
        bodyText.text = categoriesString;
        StopAllCoroutines();
        StartCoroutine(Typewriter(categoriesString));
    }
    private void ShowBuildings()
    {
        HighlightUI.defaultOptions.dismissAction = ShowBlockedArea;
        HighlightUI.ShowForUI(buildings);
        bodyText.text = buildingsString;
        StopAllCoroutines();
        StartCoroutine(Typewriter(buildingsString));
    }
    private void ShowBlockedArea()
    {
        HighlightUI.defaultOptions.dismissAction = ShowBuildTutorial;
        HighlightUI.ShowFor3DObject(blockedArea);
        bodyText.text = blockedAreaString;
        StopAllCoroutines();
        StartCoroutine(Typewriter(blockedAreaString));
    }
    private void ShowBuildTutorial()
    {
        HighlightUI.defaultOptions.dismissAction = ShowSortingTutorial;
        HighlightUI.ShowForUI(tutorialMenu);
        bodyText.text = buildingSystemString;
        StopAllCoroutines();
        StartCoroutine(Typewriter(buildingSystemString));
    }
    private void ShowSortingTutorial()
    {
        HighlightUI.defaultOptions.dismissAction = ShowCodexButton;
        HighlightUI.ShowForUI(tutorialMenu);
        bodyText.text = sortingHowToString;
        StopAllCoroutines();
        StartCoroutine(Typewriter(sortingHowToString));

        manuButtonButton.interactable = true;
        orderButtonButton.interactable = true;
        buildButtonButton.interactable = true;
        logsButton.interactable = true;
        actionButton.interactable = true;
    }
    private void ShowCodexButton()
    {
        if (!hasSeenCodexButton)
        {
            hasSeenCodexButton = true;
            HighlightUI.defaultOptions.dismissAction = ShowBellowArrow;
            HighlightUI.ShowForUI(codexButton);
            bodyText.text = codexButtonString;
            StopAllCoroutines();
            StartCoroutine(Typewriter(codexButtonString));
        }
        else
        {
            manuButtonButton.interactable = true;
            orderButtonButton.interactable = true;
            buildButtonButton.interactable = true;
            logsButton.interactable = true;
            actionButton.interactable = true;

            tutorialMenu.gameObject.SetActive(false);
            HighlightUI.Dismiss();
        }
    }

    //Button Functions
    //Activated by pressing a button first time
    public void LogsButtonPress()
    {
        if (!seenLogs)
        {
            HighlightUI.defaultOptions.dismissAction = Dismiss;
            HighlightUI.ShowForUI(logsBar);
            AssignTutorialMenu();
            StopAllCoroutines();
            StartCoroutine(Typewriter(logsString));
            seenLogs = true;
        }
        else return;

    }
    public void ActionButtonPress()
    {
        if (!seenAction)
        {
            HighlightUI.defaultOptions.dismissAction = Dismiss;
            HighlightUI.ShowForUI(actionBar);
            AssignTutorialMenu();
            StopAllCoroutines();
            StartCoroutine(Typewriter(actionBarString));
            seenAction = true;
        }
        else return;

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
    public void Dismiss()
    {
        HighlightUI.defaultOptions.dismissAction = Dismiss;
        HighlightUI.Dismiss(this);
    }
    public void TutorialSkip()
    {
        if (level == Levels.Bellow)
        {
            manuButtonButton.interactable = true;
            orderButtonButton.interactable = true;
            buildButtonButton.interactable = true;
            logsButton.interactable = true;
            actionButton.interactable = true;
        }
        if (playerMovement != null) playerMovement.SetCanMove(true);

        GameObject highlights = GameObject.Find("NoSuchStudio_HighlightCanvas");
        if (invisibleWall != null) invisibleWall.SetActive(false);
        if (highlights != null)
        {
            tutorialMenu.SetParent(GameObject.Find("UI").transform);
            tutorialMenu.gameObject.SetActive(false);
            Destroy(highlights);
        }
        HighlightUI.defaultOptions = optionsSO;
        HighlightUI.defaultOptions.dismissAction = Dismiss;


        //this.gameObject.SetActive(false);

    }
    public IEnumerator Typewriter(string text)
    {
        if (audioSource != null)
            if (!audioSource.isPlaying) audioSource.Play();
        int charCount = 0;

        typeWriterComplete = false;
        typeWriterSkip = false;

        bodyText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            if (!typeWriterSkip)
            {
                bodyText.text += letter;
                charCount++;
                yield return new WaitForSeconds(speedOfTyping);
                if (charCount == text.Length)
                { typeWriterComplete = true; }
            }
            else
            {
                bodyText.text = text;
                typeWriterComplete = true;
                break;
            }

        }
    }
    public void Hide(GameObject patient)
    {
        CanvasGroup canva = patient.GetComponent<CanvasGroup>();
        canva.alpha = 0;
        canva.blocksRaycasts = false;
        canva.interactable = false;
    }
    public void AssignTutorialMenu()
    {
        //Assigning the UI to the highlight Canvas so it fades together
        GameObject higlights = GameObject.Find("NoSuchStudio_HighlightCanvas");
        tutorialMenu.gameObject.SetActive(true);
        tutorialMenu.SetParent(higlights.transform);
    }

}
