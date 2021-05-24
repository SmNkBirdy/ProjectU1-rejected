using UnityEngine;

public class weaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;

    void Start()
    {
        switchWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            selectedWeapon = 0;
            switchWeapon();
        }

        if (Input.GetKeyDown("2"))
        {
            selectedWeapon = 1;
            switchWeapon();
        }

        if (Input.GetKeyDown("3"))
        {
            selectedWeapon = 2;
            switchWeapon();
        }
    }

    public void switchWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                GameObject.Find("gameManager").GetComponent<gameData>().activeWeapon = i;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
