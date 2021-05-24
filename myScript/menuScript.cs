using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject shop;
    public Text gems;
    public void StartGame()
    {
        SceneManager.LoadScene("demo");
    }

    public void OpenShop()
    {
        mainMenu.SetActive(false);
        shop.SetActive(true);
    }

    public void CloseShop()
    {
        mainMenu.SetActive(true);
        shop.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void returnGems()
    {
        int gemsAm = gameObject.GetComponent<gameData>().gems;
        gems.text = gemsAm.ToString();
    }

    private void Start()
    {
        returnGems();
    }
}
