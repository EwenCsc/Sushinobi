using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ParallaxItem : MonoBehaviour
{
    [HideInInspector] public float speed;
    public SpriteRenderer spriteRenderer;
    private bool isActive;

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            Move();
            OutOfScreen();
        }
    }

    void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OutOfScreen()
    {
        if(transform.position.x < - 20)
        {
            Desactivate();         
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Desactivate()
    {
        isActive = false;
        transform.localPosition = Vector3.zero;
    }
}
