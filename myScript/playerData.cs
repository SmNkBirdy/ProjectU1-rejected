using UnityEngine;
[System.Serializable]
public class playerData
{
    public int gold;
    public int gems;
    public int activeWeapon;
    public string[] weaponsNames;
    public string[] unlockWeaponsNames;

    public playerData(gameData gd)
    {
        gold = gd.gold;
        gems = gd.gems;
        activeWeapon = gd.activeWeapon;

        weaponsNames = new string[gd.weapons.Count];
        for (int i = 0; i < gd.weapons.Count; i++)
        {
            weaponsNames[i] = gd.weapons[i].name;
        }

        unlockWeaponsNames = new string[gd.unlockedWeapons.Count];
        for (int i = 0; i < gd.unlockedWeapons.Count; i++)
        {
            Debug.Log(gd.unlockedWeapons[i].name);
            unlockWeaponsNames[i] = gd.unlockedWeapons[i].name;
        }
    }
}
