using UnityEngine;

public class arenaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("gameManager").GetComponent<gameManager>().startFight(transform.position);
        }
    }
}
