using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Camera Variables
    private Quaternion characterTargetRot;
    private Quaternion cameraTargetRot;
    
    private Transform character;
    private Transform cameraTransform;
    
    public float xSensitivity = 2.0f;
    public float ySensitivity = 2.0f;
    public float minX = -90.0f;
    public float maxX = 90.0f;
    public float smoothTime = 5.0f;

    // Flags
    public bool smooth;
    public bool clampVerticalRotation = true;

    private void Start()
    {
        LockCursor(true);

        // Initialize player character references
        character = gameObject.transform;
        characterTargetRot = character.localRotation;

        // Initialize camera references
        cameraTransform = Camera.main.transform;
        cameraTargetRot = cameraTransform.localRotation;
    }

    private void Update()
    {
        if (!GameManager.gm.gameOver)                                       // If the game is not over, rotate the camera view around the mouse
        {
            LookRotation();

            if (Input.GetKey("Cancel")) LockCursor(false);                  // If the 'ESC' key is pressed, unlock the cursor
            if (Input.GetKey("Fire1")) LockCursor(true);                    // If the right-mouse button is pressed, lock the cursor
        }
        else LockCursor(false);                                             // If the game is over, unlock the cursor
    }

    // Locks/Unlocks the cursor, dependent on 'isLocked'
    private void LockCursor(bool isLocked)
    {
        if (isLocked)                                                       // Makes the cursor invisible and locks the cursor mode
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else                                                                // Makes the cursor visible and unlocks the cursor mode
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Rotates the camera view to the cursor's position
    public void LookRotation()
    {
        // Retrieve the y and x rotation from the Input Manager
        float yRot = Input.GetAxis("Mouse X") * xSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * ySensitivity;

        // Calculate the rotation
        characterTargetRot *= Quaternion.Euler(0.0f, yRot, 0.0f);
        cameraTargetRot *= Quaternion.Euler(-xRot, 0.0f, 0.0f);

        // If specified, clamp the vertical rotation
        if (clampVerticalRotation) cameraTargetRot = ClampRotationAroundXAxis(cameraTargetRot);
        

        if (smooth)                                                         // If specified, slerp the character and camera rotations
        {
            character.localRotation = Quaternion.Slerp(character.localRotation, characterTargetRot, smoothTime * Time.deltaTime);
            cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, cameraTargetRot, smoothTime * Time.deltaTime);
        }
        else                                                                // If not specified, immediately change character and camera rotations
        {
            character.localRotation = characterTargetRot;
            cameraTransform.localRotation = cameraTargetRot;
        }
    }

    // Clamps rotation of a quaternion 'q' around the X-axis
    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        // Normalize the quaternion components
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);              // Convert the x-component into an angle, in degrees
        angleX = Mathf.Clamp(angleX, minX, maxX);                           // Clamp the angle between the minimum and maximum values to restrict the rotation

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);                     // Convert the angle back into a quaternion

        return q;                                                           // Return the modified quaternion
    }
}