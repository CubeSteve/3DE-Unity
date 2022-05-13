using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera triggeredCam;
    public Camera followCam;
    public Camera liveCam;

    private void Awake()
    {
        liveCam = Camera.allCameras[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject PlayerCharcter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharcter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            triggeredCam.enabled = true;
            liveCam.enabled = false;
            followCam.enabled = false;

            liveCam = Camera.allCameras[0];

            triggeredCam.GetComponent<AudioListener>().enabled = true;
            PlayerCharcter.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject PlayerCharcter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharcter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            liveCam.enabled = true;
            triggeredCam.enabled = false;
            followCam.enabled = true;

            liveCam = followCam;

            PlayerCharcter.GetComponent<AudioListener>().enabled = true;
            triggeredCam.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        liveCam.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }
}
