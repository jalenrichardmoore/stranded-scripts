using UnityEngine;

public class FireAmmo : MonoBehaviour
{
    // Ammo Types (Light/Heavy)
    public enum AmmoType {LIGHT, HEAVY};

    // Ammo Variables
    public AmmoType currentAmmo;

    public GameObject lightAmmo;
    public GameObject heavyAmmo;

    public AudioClip ammo;

    public float lightAmmoForce = 200.0f;
    public float heavyAmmoForce = 100.0f;
    public float heavyAmmoBuffer;

    public int lightAmmoCount;
    public int maxLightAmmo;
    public int heavyAmmoCount;
    public int maxHeavyAmmo;

    private void Start()
    {
        // Initialize variables
        currentAmmo = AmmoType.LIGHT;
        heavyAmmoBuffer = Time.time + 3.0f;
        maxLightAmmo = 50;
        lightAmmoCount = 30;
        maxHeavyAmmo = 30;
        heavyAmmoCount = 20;
    }

    private void Update()
    {
        if (!GameManager.gm.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space)) SwapAmmo(currentAmmo);     // If the spacebar is pressed, swap ammo types 
            if (Input.GetMouseButtonDown(0)) Shoot(currentAmmo);            // If the left-mouse button is pressed, fire ammo
        }
    }

    // Swaps the current ammo type
    private void SwapAmmo(AmmoType current)
    {
        switch (current)
        {
            case AmmoType.LIGHT:                                            // Swaps to 'Heavy' ammo if the current ammo type is 'Light'
                currentAmmo = AmmoType.HEAVY;
                break;
            case AmmoType.HEAVY:                                            // Swaps to 'Light' ammo if the current ammo type is 'Heavy'
                currentAmmo = AmmoType.LIGHT;
                break;
        }
    }

    // Fires an ammo of the current ammo type
    public void Shoot(AmmoType current)
    {
        GameObject newAmmo;                                                 // The ammo being fired

        switch (current)
        {
            case AmmoType.LIGHT:
                if (lightAmmoCount > 0)                                     // If the player has 'Light' ammo remaining, instantiate an instance and fire it
                {
                    newAmmo = Instantiate(lightAmmo, transform.position + transform.forward, transform.rotation);
                    newAmmo.GetComponent<Rigidbody>().AddForce(transform.forward * lightAmmoForce, ForceMode.VelocityChange);
                    AudioSource.PlayClipAtPoint(ammo, newAmmo.transform.position);
                    
                    // Decrement amount of 'Light' ammo
                    lightAmmoCount--;
                }
                break;
            case AmmoType.HEAVY:
                if (heavyAmmoCount > 0 && Time.time > heavyAmmoBuffer)      // If the player has 'Heavy' ammo remaining, instantiate an instance and fire it
                {
                    newAmmo = Instantiate(heavyAmmo, transform.position + transform.forward, transform.rotation);
                    newAmmo.GetComponent<Rigidbody>().AddForce(transform.forward * heavyAmmoForce, ForceMode.VelocityChange);
                    AudioSource.PlayClipAtPoint(ammo, newAmmo.transform.position);
                
                    // Decrement amount of 'Heavy' ammo and recalculate cooldown period
                    heavyAmmoCount--;
                    heavyAmmoBuffer = Time.time + 3.0f;
                }
                break;
        }
    }
}