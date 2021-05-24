using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuGameManager : MonoBehaviour
{
    gameData gd;
    void Start()
    {
        gd = gameObject.GetComponent<gameData>();
        gd.loadGame();
    }
}
