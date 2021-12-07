using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] string playerName;

    [SerializeField] int maxLevel = 50;
    [SerializeField] int playerLevel = 1;
    [SerializeField] int currentXP;
    [SerializeField] int[] expForNextLevel;
    [SerializeField] int baseLevelXP = 100;

    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;
    [SerializeField] int maxMana = 30;
    [SerializeField] int currentMana;

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

