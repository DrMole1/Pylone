using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : MonoBehaviour
{
    public float speed = 4f;
    public Transform target;

    private bool isSeen = false;


    void OnBecameInvisible()
    {
        isSeen = false;
    }

    void OnBecameVisible()
    {
        isSeen = true;
    }

    void Update()
    {
        if(isSeen)
        {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }
}
