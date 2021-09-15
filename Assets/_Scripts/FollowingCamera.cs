using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    // ================= VARIABLES =================

    public float delay = 0.01f;
    public float distanceToAdd = 0.01f;

    // =============================================



    private void Start()
    {
        StartCoroutine(Follow());
    }

    IEnumerator Follow()
    {
        yield return new WaitForSeconds(delay);

        transform.position = new Vector3(transform.position.x, transform.position.y + distanceToAdd, -10f);

        distanceToAdd += 0.0006f;

        StartCoroutine(Follow());
    }
}
