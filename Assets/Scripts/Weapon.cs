using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponData> weaponDataList; // List of weapon data
    private WeaponData currentWeaponData;   // Current weapon data being used
    private int currentAmmo;                // Current ammo count
    private bool isReloading = false;       // Reloading status
    public Transform firePoint;             // Point from where bullets will be fired
    public string CurrentWeaponName => currentWeaponData.weaponName; // Assuming weaponData has a weaponName field
    public int CurrentAmmo => currentAmmo; 
    void Start()
    {
        // Initialize with a random weapon from the list
        SelectRandomWeapon();
        InitializeWeapon();
    }

    void InitializeWeapon()
    {
        // Set the current ammo count from the weapon data
        currentAmmo = currentWeaponData.maxAmmo;
    }

    public bool CanShoot()
    {
        return !isReloading && currentAmmo > 0;
    }
    public void AddAmmo(int amount)
{
    currentAmmo += amount; // Add the amount to the current ammo
    currentAmmo = Mathf.Clamp(currentAmmo, 0, currentWeaponData.maxAmmo); // Prevent exceeding max ammo
    Debug.Log("Ammo added: " + amount + ". Current ammo: " + currentAmmo);
}

    public void Shoot()
    {
        if (CanShoot())
        {
            AudioManager.instance.Play(currentWeaponData.sound);
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set z to 0 for 2D

            // Calculate the direction from the fire point to the mouse position
            Vector3 shootDirection = (mousePosition - firePoint.position).normalized;

            // Standardize the rotation to face the direction of the mouse
            float baseAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            // Check if the weapon behaves like a shotgun
            if (currentWeaponData.isShotgun)
            {
                // Spread shooting logic
                float angleIncrement = currentWeaponData.spreadAngle / (currentWeaponData.bulletCount - 1);
                float startAngle = baseAngle - (currentWeaponData.spreadAngle / 2); // Start from the leftmost angle

                for (int i = 0; i < currentWeaponData.bulletCount; i++)
                {
                    // Calculate the angle for each bullet
                    float bulletAngle = startAngle + (i * angleIncrement);
                    Quaternion bulletRotation = Quaternion.Euler(0, 0, bulletAngle);

                    // Instantiate the bullet
                    GameObject bullet = Instantiate(currentWeaponData.bulletPrefab, firePoint.position, bulletRotation);
                    
                    // Set bullet properties like damage, range, and size
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    bulletScript.SetProperties(
                        currentWeaponData.bulletDamage,
                        currentWeaponData.bulletRange,
                        currentWeaponData.bulletSize
                    );
                }
            }
            else
            {
                // Standard single bullet shot
                Quaternion bulletRotation = Quaternion.Euler(0, 0, baseAngle);
                GameObject bullet = Instantiate(currentWeaponData.bulletPrefab, firePoint.position, bulletRotation);
                
                // Set bullet properties for a standard weapon
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetProperties(currentWeaponData.bulletDamage, currentWeaponData.bulletRange, currentWeaponData.bulletSize);
            }

            // Decrease ammo
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
        yield return new WaitForSeconds(currentWeaponData.reloadSpeed);
        SelectRandomWeapon(); // Select a random weapon after reload
        InitializeWeapon();
        isReloading = false;
    }

    void SelectRandomWeapon()
    {
        // Select a random weapon from the list of available weapons
        int randomIndex = Random.Range(0, weaponDataList.Count);
        currentWeaponData = weaponDataList[randomIndex];
    }

    private WeaponData GenerateWeapon()
    {
        WeaponData weapon = new WeaponData();
        return weapon;
    }
}
