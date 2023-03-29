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
    [SerializeField] private bool taskComplete;
    [SerializeField] private bool seenLogs;
    [SerializeField] private bool seenCodex;
    [SerializeField] private bool seenAction;
    [SerializeField] private bool seenManu;
    [SerializeField] private bool seenManuCard;
    [SerializeField] private bool boughtManu;
    [SerializeField] private bool seenBuildButton;
    [SerializeField] private bool seenOrderButton;

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
    [SerializeField] private RectTransform manuMenu;
    [SerializeField] private RectTransform manufacturerImage;
    [SerializeField] private RectTransform manuCard;
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
    [SerializeField] private RectTransform codexMenu;
    [Space(5)]
    [SerializeField] private Animator logsAnimator;
    [SerializeField] private Animator actionAnimator;

    [SerializeField] private Button manuButtonButton;
    [SerializeField] private Button orderButtonButton;
    [SerializeField] private Button buildButtonButton;
    [SerializeField] private Button logsButton;
    [SerializeField] private Button actionButton;
    [SerializeField] private Button manuCardButton;

    [TextArea(5, 10)][SerializeField] private string introductionStr;
    [TextArea(5, 10)][SerializeField] private string logsString;
    [TextArea(5, 10)][SerializeField] private string importString;
    [TextArea(5, 10)][SerializeField] private string exportString;
    [TextArea(5, 10)][SerializeField] private string actionBarString;
    [TextArea(5, 10)][SerializeField] private string actionButtonPressedStr;
    [TextArea(5, 10)][SerializeField] private string manuButtonString;
    [TextArea(5, 10)][SerializeField] private string manuMenuStr;
    [TextArea(5, 10)][SerializeField] private string manuBuyStr;
    [TextArea(5, 10)][SerializeField] private string manuImageStr;
    [TextArea(5, 10)][SerializeField] private string manuOpenStr;
    [TextArea(5, 10)][SerializeField] private string manuCardStr;
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
                taskComplete = true;
                break;

            case Levels.Shop:
                playerMovement.SetCanMove(false);
                ShowTheItem();
                taskComplete = true;
                break;

            case Levels.Bellow:
                HighlightUI.defaultOptions.dismissAction = 
                    ShowActionBar;
                HighlightUI.ShowForUI(actionBar);
                bodyText.text = introductionStr;
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
                if (taskComplete)
                {
                    taskComplete = false;
                    HighlightUI.Dismiss();
                }
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
        taskComplete = true;
    }
    private void ShowPlayerTutorial()
    {
        audioManager.PlaySound(soundName);
        HighlightUI.defaultOptions.dismissAction = ShowLaddersTutorial;

        ChangePadding(75, 75, 100, 100);

        HighlightUI.ShowFor3DObject(player);
        StopAllCoroutines();
        StartCoroutine(Typewriter(playerString));
        taskComplete = true;
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
            taskComplete = true;
        }
        else
        {
            audioManager.PlaySound(soundName);
            tutorialMenu.gameObject.SetActive(false);
            ladders.gameObject.SetActive(false);
            HighlightUI.Dismiss();
            pauseButton.interactable = true;
            Destroy(invisibleWall);
            taskComplete = true;
        }
    }

    //Shop Quarters
    private void ShowTheItem()
    {
        HighlightUI.defaultOptions.dismissAction = ShowItemBar;
        HighlightUI.ShowFor3DObject(box);
        StopAllCoroutines();
        StartCoroutine(Typewriter(boxString));
        taskComplete = true;
    }
    private void ShowItemBar()
    {
        if (!hasPickedUpBox)
        {
            HighlightUI.ShowFor3DObject(box);
            HighlightUI.defaultOptions.dismissAction = ShowTheItem;

            StopAllCoroutines();
            StartCoroutine(Typewriter(boxString));
            taskComplete = true;
        }
        else
        {
            ChangePadding(0, 200, 400, 0);

            HighlightUI.defaultOptions.dismissAction = ShowTooltip;
            HighlightUI.ShowForUI(itemBar);
            bodyText.text = itemBarString;

            StopAllCoroutines();
            StartCoroutine(Typewriter(itemBarString));
            taskComplete = true;
        }
    }
    private void ShowTooltip()
    {
        ChangePadding(50, 50, 50, 50);

        HighlightUI.defaultOptions.dismissAction = ShowBellowArrow;
        HighlightUI.ShowFor3DObject(tooltip);

        StopAllCoroutines();
        StartCoroutine(Typewriter(tooltipString));
        taskComplete = true;
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
            taskComplete = true;
        }
        else
        {
            playerMovement.SetCanMove(true);
            audioManager.PlaySound(soundName);
            tutorialMenu.gameObject.SetActive(false);
            bellowArrow.gameObject.SetActive(false);
            HighlightUI.Dismiss();
            taskComplete = true;
        }
    }

    //Bellow

    // -Manufacturer
    private void ShowActionBar()
    {
        HighlightUI.defaultOptions.dismissAction =
            null;

        HighlightUI.ShowForUI(actionBar);

        StopAllCoroutines();
        StartCoroutine(Typewriter(actionBarString));
        
        actionButton.interactable = true;

        ChangePadding(0, 0, 0, 0);
    }
    public void PressActionBar()
    {
        if (!seenAction)
        {
            seenAction = true;
            HighlightUI.defaultOptions.dismissAction = 
                ShowManufacturerButton;

            HighlightUI.ShowForUI(actionBar);

            StopAllCoroutines();
            StartCoroutine(Typewriter(actionButtonPressedStr));

            AssignTutorialMenu();

            taskComplete=true;
        }
        else return;
    }
    private void ShowManufacturerButton()
    {
        HighlightUI.defaultOptions.dismissAction =
                PressManuButton;

        HighlightUI.ShowForUI(manuButton);

        StopAllCoroutines();
        StartCoroutine(Typewriter(manuButtonString));

        manuButtonButton.interactable = true;
    }
    public void PressManuButton()
    {
        if (!seenManu)
        {
            seenManu = true;
            HighlightUI.defaultOptions.dismissAction =
                ShowManufacturer;

            HighlightUI.ShowForUI(manuMenu);

            StopAllCoroutines();
            StartCoroutine(Typewriter(manuMenuStr));

            AssignTutorialMenu();

           taskComplete = true;
        }
        else return;
    }
    private void ShowManufacturer()
    {
        HighlightUI.defaultOptions.dismissAction =
               BuyManufacturer;

        HighlightUI.ShowForUI(manufacturerImage);

        StopAllCoroutines();
        StartCoroutine(Typewriter(manuImageStr));
    }
    public void BuyManufacturer()
    {
        if (!boughtManu)
        {
            boughtManu = true;
            manuCardButton.interactable = false;

            HighlightUI.defaultOptions.dismissAction =
            OpenManufacturerCard;

            HighlightUI.ShowForUI(manufacturerImage);

            StopAllCoroutines();
            StartCoroutine(Typewriter(manuBuyStr));

            AssignTutorialMenu();

            taskComplete=true;

        }
        else return ;
    }
    private void OpenManufacturerCard()
    {
        Debug.Log("Here");
        manuCardButton.interactable = true;

        HighlightUI.defaultOptions.dismissAction =
                Dismiss;

        HighlightUI.ShowForUI(manufacturerImage);

        StopAllCoroutines();
        StartCoroutine(Typewriter(manuOpenStr));

        taskComplete = false;
    }
    public void ShowManufacturerCard()
    {
        if (boughtManu && !seenManuCard)
        {
            seenManuCard = true;
            manuCardButton.interactable = true;

            HighlightUI.defaultOptions.dismissAction =
            Dismiss;

            HighlightUI.ShowForUI(manuCard);

            StopAllCoroutines();
            StartCoroutine(Typewriter(manuCardStr));

            AssignTutorialMenu();

            taskComplete = true;

        }
        else return;
    }

    // -Building
    public void TutorialBuildButton()
    {
        if (!seenBuildButton)
        {
            seenBuildButton = true;

            HighlightUI.defaultOptions.dismissAction =
            TutorialCategories;

            HighlightUI.ShowForUI(buildButton);

            AssignTutorialMenu();

            StopAllCoroutines();
            StartCoroutine(Typewriter(buildButtonString));
            
            taskComplete=true;
        }
        else return;    
    }
    private void TutorialCategories()
    {
        HighlightUI.defaultOptions.dismissAction = TutorialBuildings;
        HighlightUI.ShowForUI(categories);
        bodyText.text = categoriesString;
        StopAllCoroutines();
        StartCoroutine(Typewriter(categoriesString));
        taskComplete = true;
    }
    private void TutorialBuildings()
    {
        HighlightUI.defaultOptions.dismissAction = 
            Dismiss;

        HighlightUI.ShowForUI(buildings);
        bodyText.text = buildingsString;
        StopAllCoroutines();
        StartCoroutine(Typewriter(buildingsString));
        taskComplete = true;
    }
    private void AssessmentPlaceRack()
    {

    }
    private void AssessmentPlaceLabel()
    {

    }

    //  -Orders

    public void TutorialOrderButton()
    {
        if (!seenOrderButton)
        {
            seenOrderButton = true;

            HighlightUI.defaultOptions.dismissAction =
            Dismiss;

            

            HighlightUI.ShowForUI(orderButton);
            AssignTutorialMenu();
            StopAllCoroutines();
            StartCoroutine(Typewriter(orderButtonString));

            taskComplete = true;
        }
        else return;
        
    }
    private void TutorialOrderMenu()
    {

    }
    
    // -Sorting
    private void TutorialCrate()
    {
        
    }
    private void AssessmentCrate()
    {

    }
    private void AssessmentItem()
    {

    }
    private void AssessmentBag()
    {

    }

    // -Logs
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
            taskComplete=true;
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
            manuCardButton.interactable = true;
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
