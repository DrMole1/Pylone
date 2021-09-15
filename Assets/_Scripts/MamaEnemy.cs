using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaEnemy : MonoBehaviour
{
    public Transform rotator;
    public float speed = 1;

    void Update()
    {
        rotator.Rotate(0, 0, speed, Space.Self);
    }
}
