using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameShop : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public int cost = 5;
    public Material holoMaterial;
    gameData gd;
    GameObject holoWeapon;
    GameObject choosedWeapon;

    public void buy()
    {
        if (gd.gold >= cost)
        {
            Instantiate(choosedWeapon, transform.position + new Vector3(0, .5f, 0), transform.rotation);
            gd.gold -= cost;
            Destroy(holoWeapon);
        }
    }

    private void Start()
    {
        gd = GameObject.Find("gameManager").GetComponent<gameData>();
        foreach (var item in gd.allWeapons)
        {
            weapons.Add(item);
        }
        foreach (var item in gd.unlockedWeapons)
        {
            Debug.Log(item.name);
            weapons.Add(item);
        }

        //спавн голограммы
        choosedWeapon = weapons[Random.Range(0, weapons.Count)];
        holoWeapon = Instantiate(choosedWeapon,transform.position+new Vector3(0,.5f,0),transform.rotation);
        if(holoWeapon.GetComponents<weaponGiver>() != null)
        {
            Destroy(holoWeapon.GetComponent<weaponGiver>());
        }
        holoWeapon.layer = 0;
        Transform[] items = holoWeapon.GetComponentsInChildren<Transform>();
        foreach (var item in items)
        {
            if (item.GetComponent<MeshRenderer>() != null)
            {
                item.GetComponent<MeshRenderer>().material = holoMaterial;
            }
        }
        cost = choosedWeapon.GetComponent<weaponGiver>().cost;
    }
}
