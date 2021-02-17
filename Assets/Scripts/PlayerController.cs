using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool player1Selected;

    public float gravityModifier;
    public float speed;
    public float jumpForce;
    public float minGroundNormalY = 0.65f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(player1Selected == true)
            {
                player1Selected = false;
            }
            else
            {
                player1Selected = true;
            }
        }
    }
}
