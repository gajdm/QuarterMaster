using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NoSuchStudio.UI.Highlight;

public class TutorialManager2 : MonoBehaviour
{
    [SerializeField] private Text textObject;
    [SerializeField] private GameObject objectToHighlight;
    [SerializeField] private int currentItemNumber;
    [SerializeField] private bool isUI;
    [SerializeField] private List<TutorialItem> items = new List<TutorialItem>();
    [SerializeField] private List<RectTransform> rects = new List<RectTransform>();
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    [SerializeField] private TutorialItem currentItem;
    [SerializeField] private Options currentOptions;
    //START
    private void Start()
    {
        currentItemNumber = 0;
        isUI = items[currentItemNumber].uiBool;
        HighlightUI.defaultOptions.dismissAction = ChangeTutorialItem;
        ChangeTutorialItem();

    }
    //CONTROL FUNCTIONS
    public void ChangeTutorialItem()
    {
        Debug.Log("ChangeTutorialItem");

        currentItem = items[currentItemNumber];
        currentOptions = currentItem.itemOptions;
        textObject.text = currentItem.description;

        HighlightUI.defaultOptions= currentOptions;
        if (isUI) HighlightUI.ShowForUI(rects[currentItemNumber]);
        else HighlightUI.ShowFor3DObject(objects[currentItemNumber].transform);
        HighlightUI.defaultOptions.dismissAction = ChangeTutorialItem;
    }
    public void NextTutorialItem()
    {
        Debug.Log("NextTutorialItem");

        currentItemNumber++;
        isUI = items[currentItemNumber].uiBool;

        HighlightUI.defaultOptions.dismissAction = ChangeTutorialItem;
        HighlightUI.Dismiss();
    }
    public void PreviousTutorialItem()
    {
        Debug.Log("PreviousTutorialItem");

        currentItemNumber--;
        isUI = items[currentItemNumber].uiBool;

        HighlightUI.defaultOptions.dismissAction = ChangeTutorialItem;
        HighlightUI.Dismiss();
    }
    public void SkipTutorial()
    {
        HighlightUI.defaultOptions.dismissAction = null;
        HighlightUI.Dismiss();
    }
}
