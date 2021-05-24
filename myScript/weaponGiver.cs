using UnityEngine;

public class weaponGiver : MonoBehaviour
{
    public GameObject weapon;
    public Transform activeWeapon;
    public int cost;
    public int activeWeaponId;
    public int ammo;

    GameObject other;
    public void pickUp()
        {
        other = GameObject.Find("player");
        int weaponsAmount = 0;
        GameObject weaponHolder = other.transform.Find("weaponHolder").gameObject;
        weaponSwitch weaponSwitch = weaponHolder.GetComponent<weaponSwitch>();
        var weapons = weaponHolder.GetComponentsInChildren<Transform>(true);
        foreach (var weapon in weapons)
        {
            if (weapon.tag == "Weapon" || weapon.tag == "StandartWeapon")
            {
                weaponsAmount++;
                if (weapon.gameObject.activeSelf == true)
                {
                    activeWeapon = weapon;
                    activeWeaponId = weaponsAmount - 1;
                }
            }
        }
        if (weaponsAmount < 3)
        {
            var gun = Instantiate(weapon, other.transform.position + other.transform.forward * .5f, other.transform.rotation, weaponHolder.transform);
            GameObject.Find("gameManager").GetComponent<gameData>().weapons.Add(weapon);
            gun.GetComponent<weaponScript>().ammo = ammo;
            weaponSwitch.selectedWeapon = weaponsAmount;
            weaponSwitch.switchWeapon();
        }
        else
        {
            if (activeWeapon.tag != "StandartWeapon")
            {
                GameObject gameItem = Instantiate(activeWeapon.GetComponent<weaponScript>().item, gameObject.transform.position, Quaternion.identity);
                gameItem.GetComponent<weaponGiver>().ammo = activeWeapon.GetComponent<weaponScript>().ammo;
                Destroy(activeWeapon.gameObject);
                var gun = Instantiate(weapon, other.transform.position + other.transform.forward * .5f, other.transform.rotation, weaponHolder.transform);
                gun.GetComponent<weaponScript>().ammo = ammo;
                weaponSwitch.selectedWeapon = weaponsAmount;
                weaponSwitch.switchWeapon();
                gun.transform.SetSiblingIndex(activeWeaponId);
                GameObject.Find("gameManager").GetComponent<gameData>().weapons[activeWeaponId - 1] = weapon;
            }
        }
        Destroy(gameObject);
    }

}
