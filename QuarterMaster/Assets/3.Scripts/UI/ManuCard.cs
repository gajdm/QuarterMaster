using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManuCard : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ManuUI manuUI;
    [SerializeField] private CanvasGroup canvasGroup;
    

    [SerializeField] private Image icon;
    [SerializeField] private Text level;
    [SerializeField] private Text description;
    [SerializeField] private Text title;
    [SerializeField] private Text upgradeText;
    [SerializeField] private int upgradeCost;

    [SerializeField] private Image[] icons1; // Array of images for each row of items
    [SerializeField] private Image[] icons2;
    [SerializeField] private Image[] icons3;
    [SerializeField] private Image[] icons4;
    [SerializeField] private Image[] icons5;

    //Level tracking
    [SerializeField] private Image[] levels;
    [SerializeField] private Color canColor;
    [SerializeField] private Color cannotColor;

    [SerializeField] private Button upgrade;
    [SerializeField] private bool canUpgrade;

    //Helpers
    [SerializeField] private Manufacturer manuHelper;
    [SerializeField] private Sprite[] spriteHelper;
    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open(Manufacturer manufacturer)
    {
        manuHelper = manufacturer;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        //Updating the Card info

        icon.sprite = manufacturer.manuIcon;
        level.text = manufacturer.manuLevel.ToString();
        title.text = manufacturer.manuName;
        description.text = manufacturer.description;

        upgradeText.text = upgradeCost.ToString();

        foreach (Image image in levels)
        {
            image.color = cannotColor;
        }
        levels[manuHelper.manuLevel-1].color = canColor;

        //Setting up sprites
        for (int i = 0; i < manuHelper.listOfListsOfIcons.Count; i++)
        {
            SetSprites(i);
        }

    }
    public void SetSprites(int number)
    {
        spriteHelper = manuHelper.listOfListsOfIcons[number].GetSprites();
        switch (number)
        {
            case 0:
                for (int i = 0; i < icons1.Length; i++)
                {
                    icons1[i].sprite = spriteHelper[i];
                }
                break;
            case 1:
                for (int i = 0; i < icons1.Length; i++)
                {
                    icons2[i].sprite = spriteHelper[i];
                }
                break;
            case 2:
                for (int i = 0; i < icons1.Length; i++)
                {
                    icons3[i].sprite = spriteHelper[i];
                }
                break;
            case 3:
                for (int i = 0; i < icons1.Length; i++)
                {
                    icons4[i].sprite = spriteHelper[i];
                }
                break;
            case 4:
                for (int i = 0; i < icons1.Length; i++)
                {
                    icons5[i].sprite = spriteHelper[i];
                }
                break;
            default:
                return;

        } 
    }
    public void Upgrade()
    {
        if (gameManager.GetGoldCurrent() >= upgradeCost)
        {
            gameManager.PayGold(upgradeCost);
            manuHelper.manuLevel++;
            Open(manuHelper);
            manuUI.UpdateUI();
            levels[manuHelper.manuLevel - 1].color = canColor;
        }
        
    }

}
