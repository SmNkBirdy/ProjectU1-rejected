using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopSlotBuy : MonoBehaviour
{
    public GameObject weapon;
    public int cost;
    gameData gd;
    menuScript ms;
    public Text nameText;
    public Text costText;


    public void buy()
    {
        if (gd.gems >= cost)
        {
            gd.gems -= cost;
            ms.returnGems();
            gd.unlockedWeapons.Add(weapon);
            gameObject.SetActive(false);
            gd.saveGame();
        }
    }

    public bool checkWeaponStatus()
    {
        List<GameObject> unlockedWeapons = gd.unlockedWeapons;
        foreach (var item in unlockedWeapons)
        {
            if (item == weapon)
            {
                return true;
            }
        }
        return false;
    }

    private void Start()
    {
        gd = GameObject.Find("menuGameManager").GetComponent<gameData>();
        ms = GameObject.Find("menuGameManager").GetComponent<menuScript>();
        nameText.text = "Оружие:" + weapon.name;
        costText.text = "Цена:" + cost;
        if (checkWeaponStatus())
        {
            gameObject.SetActive(false);
        }
    }
}
