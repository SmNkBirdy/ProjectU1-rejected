using UnityEngine;

public class gatesScript : MonoBehaviour
{
    public GameObject barrier;

    public void open()
    {
        barrier.SetActive(false);
    }

    public void close()
    {
        barrier.SetActive(true);
    }
}
