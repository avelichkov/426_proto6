using UnityEngine;

public class AmmoBoxSpawner : MonoBehaviour
{
    public GameObject ammoBoxPrefab; // Reference to the ammo box prefab
    public float spawnInterval = 10f; // How often to spawn an ammo box
    public Vector2 spawnAreaMin; // Min position for spawning
    public Vector2 spawnAreaMax; // Max position for spawning

    private void Start()
    {
        // Start spawning ammo boxes
        InvokeRepeating(nameof(SpawnAmmoBox), spawnInterval, spawnInterval);
    }

    void SpawnAmmoBox()
    {
        // Generate a random position within the defined area
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Instantiate the ammo box at the random position
        Instantiate(ammoBoxPrefab, randomPosition, Quaternion.identity);
    }
}