using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCarpet : MonoBehaviour {

    [SerializeField] private Sprite sprite = null;

    [SerializeField] private GameObject[] elements = new GameObject[0];
    [SerializeField] private float speed = 0.0f;

    private Vector3 size = Vector3.zero;

    private void Start()
    {
        size = sprite.bounds.size;

        //Set all elements out of screen
        for (int i = 0; i < elements.Length; i++)
        {
            Vector3 position = elements[i].transform.position;
            elements[i].transform.position = new Vector3(size.x * i, position.y, 0.0f);
        }
    }

    private void Update()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            Vector3 offset = Vector3.left * speed * Time.deltaTime;

            //If out of screen, reset behind last element
            if (elements[i].transform.position.x < -size.x)
            {
                Vector3 position = Vector3.zero;

                if (i == elements.Length - 1)
                {
                    position = elements[0].transform.position - offset;
                }
                else
                {
                    position = elements[i + 1].transform.position;
                }

                position.x += size.x;
                elements[i].transform.position = position;
            }

            //Move elements
            elements[i].transform.Translate(offset);
        }
    }
}
