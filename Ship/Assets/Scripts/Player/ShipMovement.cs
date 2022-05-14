using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public GameObject cam2;

    public GameObject clawBody;
    public GameObject clawMaxHeight;

    public GameObject finger1;
    public GameObject finger2;
    public GameObject finger3;
    public GameObject finger4;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;

        lr.SetPosition(0, clawMaxHeight.transform.position);
        lr.SetPosition(1, clawBody.transform.position);
    }

    void Update()
    {
        if (!player.activeInHierarchy)
        {
            bool enter = Input.GetButtonDown("Enter Ship");

            if (enter)
            {
                player.SetActive(true);
                player.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                cam2.SetActive(false);
                cam.SetActive(true);
                cam.GetComponent<FollowCamera>().target = player;
                return;
            }

            float v = Input.GetAxis("Ship Vertical");
            float t = Input.GetAxis("Ship Turn");
            float u = Input.GetAxis("Ship Up");
            float clawU = Input.GetAxis("Ship Claw Up");
            float clawA = Input.GetAxis("Ship Arm Up");

            CollisionDetecter(transform);

            Rotating(t);
            MovementManager(v, u);

            ClawManager(clawU);

            FingerManager(finger1, finger2, finger3, finger4, clawA);

            CamManager();
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

    void CollisionDetecter(Transform target)
    {
        //Collision detection
        if (target.transform.position.y < 0 && target.transform.position.y > -3 &&
            target.transform.position.x < 35 && target.transform.position.x > -35 &&
            target.transform.position.z < 35 && target.transform.position.z > -35)
        {
            target.transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z);
            return;
        }
    }

    void ClawManager(float up)
    {
        clawBody.transform.position += transform.up * Time.deltaTime * up;
        CollisionDetecter(clawBody.transform);

        if (clawBody.transform.position.y > clawMaxHeight.transform.position.y)
        {
            clawBody.transform.position = new Vector3(clawBody.transform.position.x, clawMaxHeight.transform.position.y, clawBody.transform.position.z);
        }

        lr.SetPosition(0, clawMaxHeight.transform.position);
        lr.SetPosition(1, clawBody.transform.position);
    }

    void FingerManager(GameObject a, GameObject b, GameObject c, GameObject d, float clawArm)
    {
        //a.transform.rotation = new Quaternion(a.transform.rotation.x, a.transform.rotation.y, a.transform.rotation.z, a.transform.rotation.w);
        //b.transform.rotation = new Quaternion(b.transform.rotation.x, b.transform.rotation.y, b.transform.rotation.z, b.transform.rotation.w);
        //c.transform.rotation = new Quaternion(c.transform.rotation.x, c.transform.rotation.y, c.transform.rotation.z, c.transform.rotation.w);
        //d.transform.rotation = new Quaternion(d.transform.rotation.x, d.transform.rotation.y, d.transform.rotation.z, d.transform.rotation.w);

        Rigidbody ourBody;
        Quaternion deltaRotation;

        if (a.transform.rotation.x < -0.4f && clawArm < 0 ||
            a.transform.rotation.x > 0 && clawArm > 0)
        {
            return;
        }

        //Access rigidbody
        ourBody = a.GetComponent<Rigidbody>();
        deltaRotation = Quaternion.Euler(clawArm * 0.25f, 0f, 0f);
        ourBody.MoveRotation(ourBody.rotation * deltaRotation);

        ourBody = b.GetComponent<Rigidbody>();
        deltaRotation = Quaternion.Euler(clawArm * 0.25f, 0f, 0f);
        ourBody.MoveRotation(ourBody.rotation * deltaRotation);

        ourBody = c.GetComponent<Rigidbody>();
        deltaRotation = Quaternion.Euler(clawArm * 0.25f, 0f, 0f);
        ourBody.MoveRotation(ourBody.rotation * deltaRotation);

        ourBody = d.GetComponent<Rigidbody>();
        deltaRotation = Quaternion.Euler(clawArm * 0.25f, 0f, 0f);
        ourBody.MoveRotation(ourBody.rotation * deltaRotation);
    }

    void CamManager()
    {
        if (clawBody.transform.position.y < clawMaxHeight.transform.position.y - 3)
        {
            cam.SetActive(false);
            cam2.SetActive(true);
        }
        if (clawBody.transform.position.y > clawMaxHeight.transform.position.y - 3)
        {
            cam.SetActive(true);
            cam2.SetActive(false);
        }
    }
}
