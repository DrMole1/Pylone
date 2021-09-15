using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour
{
    public enum ColorType { Blue, Red, Green };

    // ================ VARIABLES ================

    public HeadCharge headCharge;

    public ColorType color;

    public bool isLast = false;

    // ===========================================


    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject && headCharge.isOnMovement == false)
                {
                    headCharge.nextPylonToMove = gameObject.GetComponent<Pylon>();
                    headCharge.Move();
                }
            }
        }
    }
}
