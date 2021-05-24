using UnityEngine;

public class playerFollow : MonoBehaviour
{
    [Header("Importing")]
    public GameObject player;
    [Header("Variables")]
    Quaternion standartRotation;
    public Vector3 cameraStandartPosition = new Vector3(0, 8.5f, -4);
    public float xMultiplier = 0.005f;
    public float yMultiplier = 0.008f;
    bool showMap;

    public void moveCamera()
    {
        transform.position = player.transform.position + cameraStandartPosition + new Vector3((Input.mousePosition.x - Screen.width/2) * xMultiplier, 0, (Input.mousePosition.y - Screen.height /2) * yMultiplier);
        transform.rotation = standartRotation;
    }

    public void changeMapMode()
    {
        transform.position = new Vector3(0, 200, 0);
        transform.LookAt(new Vector3(0,0,0));
        showMap = !showMap;
    }

    private void Start()
    {
        player = GameObject.Find("player");
        standartRotation = transform.rotation;
        moveCamera();
    }
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            changeMapMode();
        }
        if (!showMap)
        {
            moveCamera();
        }
    }
}
