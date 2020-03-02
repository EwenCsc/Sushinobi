using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset = Vector3.zero;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = player.transform.position + offset;
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Deadly")
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Deadly")
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //    }
    //}
}
