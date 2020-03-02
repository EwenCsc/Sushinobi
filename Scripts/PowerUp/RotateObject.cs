using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float speed = 0.0f;

    private void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
