using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    [SerializeField] GameObject menu;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, levelText, expText;
    [SerializeField] Slider[] expSlider;
    [SerializeField] Image[] characterImage;
    [SerializeField] GameObject[] characterPanel;


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

            hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
            manaText[i].text = "Mana: " + playerStats[i].currentMana + "/" + playerStats[i].maxMana;

            levelText[i].text = "Level: " + playerStats[i].playerLevel;
            expSlider[i].maxValue = playerStats[i].expForNextLevel[playerStats[i].playerLevel];
            expSlider[i].value = playerStats[i].currentXP;
            expText[i].text = playerStats[i].currentXP + "/" + playerStats[i].expForNextLevel[playerStats[i].playerLevel];

            characterImage[i].sprite = playerStats[i].characterImage;
        }
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Fade Start");
    }
}
