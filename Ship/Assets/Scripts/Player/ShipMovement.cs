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
            float u = Input.GetAxis("Ship Up");

            CollisionDetecter();

            Rotating(t);
            MovementManager(v, u);
        }
    }

    void MovementManager(float vertical, float up)
    {
        transform.position += transform.forward * Time.deltaTime * vertical * 10;

        transform.position += transform.up * Time.deltaTime * up * 2;
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

    void CollisionDetecter()
    {
        //Collision detection
        if (transform.position.y < 0 && transform.position.y > -3 &&
            transform.position.x < 35 && transform.position.x > -35 &&
            transform.position.z < 35 && transform.position.x > -35)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            return;
        }
    }
}
