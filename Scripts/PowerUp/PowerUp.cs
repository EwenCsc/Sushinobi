/* 
 * @author Garnier Gael
 * @date 10/10/2018
 * @file PowerUp.cs
 */

using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [HideInInspector] public bool isCollected = false;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (gameObject.GetComponent<Collider2D>().bounds.Contains(touchPosition))
                {
                    Debug.Log("Ok");
                    OnCollect();
                }
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        // Play FX, Sound, etc ...
        AudioManager.Instance.PlaySound("PowerUp");
        isCollected = true;
    }
}
