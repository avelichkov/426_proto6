using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 10; // Amount of ammo to give

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that touched the box is the player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with AmmoBox.");

            // Log the names of child objects for debugging
            foreach (Transform child in collision.transform)
            {
                Debug.Log("Child object: " + child.name + " | Has Weapon: " + (child.GetComponent<Weapon>() != null));
            }

            // Get the Weapon script attached to the player's Weapon GameObject
            Weapon playerWeapon = collision.GetComponentInChildren<Weapon>();

            if (playerWeapon != null)
            {
                // Add ammo to the player's current weapon
                playerWeapon.AddAmmo(ammoAmount);
                Debug.Log("Picked up ammo box. Ammo added: " + ammoAmount);

                // Destroy the ammo box after it has been used
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("No Weapon component found in the child's Weapon object.");
            }
        }
    }
}
