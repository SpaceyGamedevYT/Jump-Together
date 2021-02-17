using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public void Footstep()
    {
        AudioManager.instance.Play("step");
    }

    public void Jump()
    {
        AudioManager.instance.Play("jump");
    }

    public void Land()
    {
        AudioManager.instance.Play("land");
    }
}