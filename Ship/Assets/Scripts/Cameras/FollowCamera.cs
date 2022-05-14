using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;
    public GameObject player;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

        if (player.activeInHierarchy)
        {
            transform.position = target.transform.position + (rotation * offset);
        }
        else
        {
            //Ship
            transform.position = target.transform.position + (rotation * offset * 2);
        }

        transform.LookAt(target.transform);
    }
}
