using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Rotation Variables
    [SerializeField] private float degrees = 0.9f;

    private void Update()
    {
        transform.Rotate(Vector3.up * degrees, Space.Self);                 // Rotate the game object around the world Y-axis by 'degrees' degrees
    }
}