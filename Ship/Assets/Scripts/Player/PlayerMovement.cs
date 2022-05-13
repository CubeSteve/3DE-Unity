using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Parameters
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f; //Prevents jumps in animation
    public float sensitivityX = 1.0f;

    public float animationSpeed = 1.5f;

    private Animator anim;
    private HashIDs hash;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();

        anim.SetLayerWeight(1, 1f);
    }

    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        float turn = Input.GetAxis("Turn");

        Rotating(turn);
        MovementManager(v, sneak);
    }

    void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout);
    }

    void MovementManager(float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        //Check if pressing the up or down arrow keys
        if (vertical > 0)
        {
            anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
        }
        else
        {
            anim.SetFloat(hash.speedFloat, 0);
        }
    }

    void Rotating(float mouseXInput)
    {
        //Access rigidbody
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        //Check if there is rotation to be applied
        if (mouseXInput != 0)
        {
            //Use mouseX to create a Euler angle to rotate in the Y axis
            //Turn into a Quaternion
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);

            //Apply to the rigidbody
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }

    void AudioManagement(bool shout)
    {
        //If walking and audio is not playing
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("walk") || !GetComponent<AudioSource>().isPlaying)
        {
            //Play audio
            GetComponent<AudioSource>().pitch = 0.27f;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            //Stop audio
            GetComponent<AudioSource>().Stop();
        }

        //Check if shout was called
        if(shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}
