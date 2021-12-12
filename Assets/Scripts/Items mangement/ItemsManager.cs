using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public enum ItemType {Item, Weapon, Armor}
    public ItemType itemType;

    public string itemName, itemDescription;
    public int valueCoins;
    public Sprite itemsImage;

    public int amoutOfAffect;

    public enum AffectType { HP, Mana}
    public AffectType affectType;

    public int weaponDexterity, armorDefense;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.instance.AddItems(this);
            SelftDestruct();
        }
    }

    public void SelftDestruct()
    {
        Destroy(gameObject);
    }
}
