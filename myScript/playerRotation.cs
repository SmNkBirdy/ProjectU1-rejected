using UnityEngine;

public class playerRotation : MonoBehaviour
{

    [Header("Importing")]
    public Camera mainCamera;

    void Update()
    {
        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        float hitDist;
        Plane playerPlane = new Plane(transform.up, transform.position);
        playerPlane.Raycast(mouseRay, out hitDist);
        Vector3 mousePoint = mouseRay.GetPoint(hitDist);
        Quaternion playerRotation = Quaternion.LookRotation(mousePoint - transform.position);
        transform.rotation = playerRotation;
    }

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}
