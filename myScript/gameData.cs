using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameData : MonoBehaviour
{
    public int gold;
    public int gems;

    public int activeWeapon;
    public List<GameObject> weapons = new List<GameObject>();

    public List<GameObject> allWeapons = new List<GameObject>();
    public List<GameObject> lockedWeapons = new List<GameObject>();
    public List<GameObject> unlockedWeapons = new List<GameObject>();

    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> lockedItems = new List<GameObject>();
    public List<GameObject> unlockedItems = new List<GameObject>();

    public List<GameObject> lockedCharacters = new List<GameObject>();
    public List<GameObject> unlockedCharacters = new List<GameObject>();



    // рвбота с сейвами

    public void resetGameSession()
    {
        gold = 0;
        activeWeapon = 0;
        weapons.Clear();
    }

    public void saveGame()
    {
        saveSystem.SaveSystem(gameObject.GetComponent<gameData>());
    }

    public void loadGame()
    {
        playerData data = saveSystem.LoadSystem();
        if (data != null)
        {
            var gm = GameObject.Find("gameManager");
            if (gm != null)
            {
                gems = data.gems;
                GameObject wp = GameObject.Find("weaponHolder");
                GameObject pl = GameObject.Find("player");
                foreach (var item in data.unlockWeaponsNames)
                {
                    bool newWeaponCheck = true;
                    for (int i = 0; i < unlockedWeapons.Count; i++)
                    {
                        if (unlockedWeapons[i].GetComponent<weaponGiver>().weapon.name == item)
                        {
                            newWeaponCheck = false;
                        }
                    }
                    if (newWeaponCheck)
                    {
                        GameObject newWeapon = null;
                        foreach (var anotherItem in lockedWeapons)
                        {
                            if (anotherItem.name == item)
                            {
                                newWeapon = anotherItem;
                            }
                        }
                        unlockedWeapons.Add(newWeapon);
                    }
                }
                if (GameObject.Find("gameManager").GetComponent<gameManager>().currentLevel != 1)
                {
                    gold = data.gold;
                    foreach (var item in data.weaponsNames)
                    {
                        for (int i = 0; i < allWeapons.Count; i++)
                        {
                            if (allWeapons[i].GetComponent<weaponGiver>().weapon.name == item)
                            {
                                Instantiate(allWeapons[i].GetComponent<weaponGiver>().weapon, pl.transform.position + pl.transform.forward * .5f, pl.transform.rotation, wp.transform);
                                GameObject.Find("gameManager").GetComponent<gameData>().weapons.Add((allWeapons[i].GetComponent<weaponGiver>().weapon));
                            }
                        }
                    }
                    wp.GetComponent<weaponSwitch>().selectedWeapon = data.activeWeapon;
                    wp.GetComponent<weaponSwitch>().switchWeapon();
                }
            }

            gm = GameObject.Find("menuGameManager");

            if (gm != null)
            {
                foreach (var item in data.unlockWeaponsNames)
                {
                    Debug.Log(item);
                    bool newWeaponCheck = true;
                    for (int i = 0; i < unlockedWeapons.Count; i++)
                    {
                        if (unlockedWeapons[i].GetComponent<weaponGiver>().weapon.name == item)
                        {
                            newWeaponCheck = false;
                        }
                    }
                    if (newWeaponCheck)
                    {
                        GameObject newWeapon = null;
                        foreach (var anotherItem in lockedWeapons)
                        {
                            if (anotherItem.name == item)
                            {
                                newWeapon = anotherItem;
                            }
                        }
                        unlockedWeapons.Add(newWeapon);
                    }
                }
                gems = data.gems;
            }

        }
    }
}
