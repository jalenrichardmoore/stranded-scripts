using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player Variables
    public float speed = 3.0f;

    private void Update()
    {
        if (!GameManager.gm.gameOver)
        {
            // Move the player in the appropriate direction by 'speed' units
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}