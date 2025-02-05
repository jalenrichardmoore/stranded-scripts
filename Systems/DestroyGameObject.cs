using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    // Timer Variables
    public float timeToWait;

    private void Awake()
    {
        Invoke("DestroyNow", timeToWait);                                   // Destroys the game object after 'timeToWait' seconds
    }

    // Destroys the game object
    public void DestroyNow() 
    {
        Destroy(gameObject);
    }
}