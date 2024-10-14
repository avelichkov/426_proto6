using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName; // Name of the weapon
    public int maxAmmo; // Maximum ammo capacity
    public int bulletDamage; // Damage dealt by each bullet
    public float bulletRange; // Range of each bullet
    public float reloadSpeed; // Speed of reloading
    public float bulletSize; // Size of the bullet
    public GameObject bulletPrefab; // Bullet prefab reference
    public bool isShotgun; // Indicates if this weapon behaves like a shotgun
    public int bulletCount; // Number of bullets fired for shotgun
    public float spreadAngle; // Angle spread for shotgun pellets
}