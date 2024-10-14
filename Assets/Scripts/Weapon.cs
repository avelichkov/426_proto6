using System.Collections.Generic; // Include for List
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponData> weaponDataList; // List of weapon data
    private int currentWeaponIndex = 0; // Track the currently equipped weapon
    private int currentAmmo; // Current ammo count
    private bool isReloading = false; // Reloading status
    public Transform firePoint; // Point from where bullets will be fired

    void Start()
    {
        InitializeWeapon();
    }

    void InitializeWeapon()
    {
        // Initialize the current weapon's ammo count from the weapon data list
        currentAmmo = weaponDataList[currentWeaponIndex].maxAmmo;
    }

    public bool CanShoot()
    {
        return !isReloading && currentAmmo > 0; // Check if can shoot
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set z to 0 for 2D

            // Calculate the direction from the fire point to the mouse position
            Vector3 shootDirection = (mousePosition - firePoint.position).normalized;

            // Check if the weapon behaves like a shotgun
            if (weaponDataList[currentWeaponIndex].isShotgun)
            {
                // Calculate the angle increment based on the total spread angle
                float angleIncrement = weaponDataList[currentWeaponIndex].spreadAngle / (weaponDataList[currentWeaponIndex].bulletCount - 1);
                float startAngle = -weaponDataList[currentWeaponIndex].spreadAngle / 2; // Start from the leftmost angle

                for (int i = 0; i < weaponDataList[currentWeaponIndex].bulletCount; i++)
                {
                    // Calculate the angle for each bullet
                    float angle = startAngle + (i * angleIncrement);

                    // Create a rotation based on the shoot direction and the calculated angle
                    Quaternion rotation = Quaternion.Euler(0, 0, angle) * Quaternion.LookRotation(Vector3.forward, shootDirection);

                    // Instantiate the bullet
                    GameObject bullet = Instantiate(weaponDataList[currentWeaponIndex].bulletPrefab, firePoint.position, rotation);
                    
                    // Set bullet properties
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    bulletScript.SetProperties(weaponDataList[currentWeaponIndex].bulletDamage, weaponDataList[currentWeaponIndex].bulletRange, weaponDataList[currentWeaponIndex].bulletSize);
                }
            }
            else
            {
                // Standard single bullet shot
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
                GameObject bullet = Instantiate(weaponDataList[currentWeaponIndex].bulletPrefab, firePoint.position, rotation);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetProperties(weaponDataList[currentWeaponIndex].bulletDamage, weaponDataList[currentWeaponIndex].bulletRange, weaponDataList[currentWeaponIndex].bulletSize);
            }

            currentAmmo--;
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponDataList[currentWeaponIndex].reloadSpeed);
        UpgradeWeapon(); // Switch to the next weapon after reloading
        isReloading = false;
    }

    void UpgradeWeapon()
    {
        // Check if there's another weapon to switch to
        if (currentWeaponIndex < weaponDataList.Count - 1)
        {
            currentWeaponIndex++; // Switch to the next weapon
        }
        else
        {
            currentWeaponIndex = 0; // If at the end, loop back to the first weapon
        }

        InitializeWeapon(); // Re-initialize ammo for the new weapon
    }
}
