using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    [SerializeField] GameObject menu;

    [SerializeField] GameObject[] statsButtons;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, levelText, expText;
    [SerializeField] Slider[] expSlider;
    [SerializeField] Image[] characterImage;
    [SerializeField] GameObject[] characterPanel;

    [SerializeField] TextMeshProUGUI statHP, statName, statMana, statDef, statDex;
    [SerializeField] Image characterStatImage;

    [SerializeField] Transform itemSlotContainerParent;
    [SerializeField] GameObject itemSlotContainer;

    public static MenuManager instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (menu.activeInHierarchy)
            {
                menu.SetActive(false);
                GameManager.instance.gameMenuOpened = false;
            }
            else
            {
                UpdateStats();
                menu.SetActive(true);
                GameManager.instance.gameMenuOpened = true;
            }
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();

        for (int i = 0; i < playerStats.Length; i++)
        {
            characterPanel[i].SetActive(true);

            nameText[i].text = playerStats[i].playerName;

            hpText[i].text = "HP: " + playerStats[i].currentHP + " / " + playerStats[i].maxHP;
            manaText[i].text = "Mana: " + playerStats[i].currentMana + " / " + playerStats[i].maxMana;

            levelText[i].text = "Level: " + playerStats[i].playerLevel;
            expSlider[i].maxValue = playerStats[i].expForNextLevel[playerStats[i].playerLevel];
            expSlider[i].value = playerStats[i].currentXP;
            expText[i].text = playerStats[i].currentXP + " / " + playerStats[i].expForNextLevel[playerStats[i].playerLevel];

            characterImage[i].sprite = playerStats[i].characterImage;
        }
    }

    public  void UpdateItemsInventory()
    {
        foreach(Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot);
        }

        foreach(ItemsManager item in Inventory.instance.GetItemsList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("Item Image").GetComponent<Image>();
            itemImage.sprite = item.itemsImage;

            TextMeshProUGUI itemText = itemSlot.Find("Amount Text").GetComponent<TextMeshProUGUI>();

            if (item.isStackable)
            {
                itemText.text = item.amount.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void StatsMenu()
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            statsButtons[i].SetActive(true);
            statsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].playerName;
        }

        StatsMenuUpdate(0);
    }

    public void StatsMenuUpdate(int playerSelectedNumber)
    {
        PlayerStats playerSelected = playerStats[playerSelectedNumber];

        statName.text = playerSelected.playerName;
        statHP.text = playerSelected.currentHP + " / " + playerSelected.maxHP;
        statMana.text = playerSelected.currentMana + " / " + playerSelected.maxMana;

        statDex.text = playerSelected.dexterity.ToString();
        statDef.text = playerSelected.defense.ToString();

        characterStatImage.sprite = playerSelected.characterImage;
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Fade Start");
    }

    public void QuitGame()
    {
        Application.Quit();
        print("We've quitted the game.");
    }
}
