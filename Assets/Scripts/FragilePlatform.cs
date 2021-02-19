using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    int collisionCount = 0;
    bool hasTouchedPlatform;
    public SpriteRenderer spriteRenderer;
    public Sprite crackedBlock;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        collisionCount++;
        hasTouchedPlatform = true;
        spriteRenderer.sprite = crackedBlock;
        AudioManager.instance.Play("crack");
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        collisionCount--;
    }

    private void Update() 
    {
        if(collisionCount <= 0 && hasTouchedPlatform)
        {
            Destroy(gameObject);
            AudioManager.instance.Play("break");
        }
    }
}