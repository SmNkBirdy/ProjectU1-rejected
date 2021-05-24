using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteractions : MonoBehaviour
{
    public float pickUpRadius = 10f;
    public LayerMask usableMask;
    GameObject closestItem = null;
    // Update is called once per frame
    void Update()
    {
        if (closestItem != null)
        {
            Debug.Log(closestItem.name);
            if (Vector3.Distance(gameObject.transform.position, closestItem.transform.position) > pickUpRadius)
            {
                closestItem = null;
            }
        }
        Collider[] items = Physics.OverlapSphere(gameObject.transform.position, pickUpRadius, usableMask);
        float minDist = pickUpRadius + 1;
        foreach (Collider item in items)
        {
            if (Vector3.Distance(gameObject.transform.position, item.transform.position) < minDist)
            {
                closestItem = item.gameObject;
                minDist = Vector3.Distance(gameObject.transform.position, item.transform.position);
            }
        }

        if (Input.GetKeyDown("e"))
        {
            Debug.Log("test!");
            if (closestItem != null)
            {
                weaponGiver wg = closestItem.GetComponent<weaponGiver>();
                if (wg != null)
                {
                    wg.pickUp();
                }
                chestScript cs = closestItem.GetComponent<chestScript>();
                if (cs != null)
                {
                    cs.open();
                }
                inGameShop igs = closestItem.GetComponent<inGameShop>();
                if (igs != null)
                {
                    igs.buy();
                }

            }

        }
    }
}
