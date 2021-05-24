using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    gameData gd;
    public List<GameObject> weapons = new List<GameObject>();
    // Start is called before the first frame update
    
    public void open()
    {
        Instantiate(weapons[Random.Range(0, weapons.Count)], gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
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
    }
}
