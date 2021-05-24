using UnityEngine;

public class objectHealth : MonoBehaviour
{
    public GameObject gold;
    [Header("Variables")]
    public float healthPoint = 6;

    // Update is called once per frame
    void Update()
    {
        if (healthPoint < 1 && gameObject.tag != "Player")
        {
            if (gameObject.tag == "Enemy")
            {
                int dropRate = Random.Range(0,101);
                if (dropRate > 80)
                {
                    Instantiate(gold, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
        else if(healthPoint < 1 && gameObject.tag == "Player")
        {
            GameObject.Find("gameManager").GetComponent<gameManager>().restartSession();
        }
    }
}
