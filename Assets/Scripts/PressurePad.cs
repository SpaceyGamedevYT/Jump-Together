using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public bool touchingPad;
    public Animator anim;
    public string thisPadColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(thisPadColor))
        {
            touchingPad = true;
            anim.SetTrigger("lower");
            AudioManager.instance.Play("pad");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(thisPadColor))
        {
            touchingPad = false;
            anim.SetTrigger("raise");
        }
    }
}
