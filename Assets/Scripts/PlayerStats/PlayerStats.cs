using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public string playerName;

     public Sprite characterImage;

    [SerializeField] int maxLevel = 50;
    public int playerLevel = 1;
    public int currentXP;
    public int[] expForNextLevel;
    [SerializeField] int baseLevelXP = 100;

    public int maxHP = 100;
    public int currentHP;
    public int maxMana = 30;
    public int currentMana;

    [SerializeField] int dexterity;
    [SerializeField] int defense;

    // Start is called before the first frame update
    void Start()
    {
        expForNextLevel = new int[maxLevel];
        expForNextLevel[1] = baseLevelXP;

        for (int i = 2; i < expForNextLevel.Length; i++)
        {
            expForNextLevel[i] = (int)(0.02f * i * i * i + 3.06f * i * i + 105.6f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddXP(100);
        }
    }

    public void AddXP(int amountOfXp)
    {
        currentXP += amountOfXp;
        if(currentXP > expForNextLevel[playerLevel])
        {
            currentXP -= expForNextLevel[playerLevel];
            playerLevel++;

            if(playerLevel % 2 == 0)
            {
                dexterity++;
            }
            else
            {
                defense++;
            }

            maxHP = (int)(maxHP * 1.18f);
            currentHP = maxHP;

            maxMana = (int)(maxMana * 1.06f);
            currentMana = maxMana;
        }
    }
}

