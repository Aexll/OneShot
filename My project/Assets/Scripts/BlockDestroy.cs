using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockDestroy : MonoBehaviour
{
    //public Sprite sprite1;
    //public Sprite sprite2;
    //public Sprite sprite3;
    public List<Sprite> sprites;
    public SpriteRenderer render;
    public int health = 3;

    public UnityEvent EventDestroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        health--;
        if (sprites.Count > health)
        {
            render.sprite = sprites[health];
        }
        if(health <= 0)
        {
            EventDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }


}
