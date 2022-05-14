using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (!player.activeInHierarchy)
        {
            float v = Input.GetAxis("Ship Vertical");
            float t = Input.GetAxis("Ship Turn");
            float up = Input.GetAxis("Ship Up");

            Rotating(t);
            MovementManager(v);
        }
    }

    void MovementManager(float vertical)
    {
        //Access rigidbody
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        ourBody.MovePosition(transform.position + new Vector3(0, 0, vertical));

        // transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }

    void Rotating(float turn)
    {
        //Access rigidbody
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        //Check if there is rotation to be applied
        if (turn != 0)
        {
            //Use mouseX to create a Euler angle to rotate in the Y axis
            //Turn into a Quaternion
            Quaternion deltaRotation = Quaternion.Euler(0f, turn * 0.25f, 0f);

            //Apply to the rigidbody
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }
}
