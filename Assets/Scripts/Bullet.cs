using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed; // Speed of the bullet
    private Rigidbody2D _rb; // Reference to Rigidbody2D
    private float _damage; // Damage dealt by the bullet
    private float _range; // Range of the bullet
    private float _size; // Size of the bullet
    private Vector3 _startPosition; // Starting position of the bullet
    

    void Awake()
    {
        // Get the Rigidbody2D component attached to this bullet
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Set the bullet's velocity based on the speed and direction
        _rb.velocity = transform.right * speed;

        // Optionally, set the size of the bullet
        transform.localScale = Vector3.one * _size;

        // Destroy the bullet after it has traveled its range
        Destroy(gameObject, _range / speed); // Adjust as needed based on your range and speed
    }

    public void SetProperties(float damage, float range, float size)
    {
        _damage = damage; // Set the damage value
        _range = range; // Set the range value
        _size = size; // Set the size value

        // Set the bullet's initial velocity
        _rb.velocity = transform.right * speed;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     // Check for collisions with other objects
    //     if (other.CompareTag("Enemy")) // Change to whatever tag your enemies have
    //     {
    //         // Handle enemy damage here
    //         Enemy enemy = other.GetComponent<Enemy>(); // Assuming an Enemy script exists
    //         if (enemy != null)
    //         {
    //             enemy.TakeDamage(_damage); // Call a method to damage the enemy
    //         }

    //         // Destroy the bullet upon hitting an enemy
    //         Destroy(gameObject);
    //     }

    //     // Optionally, destroy the bullet on hitting any other object
    //     if (!other.CompareTag("Player")) // Prevent bullet from destroying on player collision
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    public void TakeDamage(float damage)
    {
        // If the bullet had health, reduce it here
        // For simplicity, we'll just destroy it directly in this case
        Destroy(gameObject);
    }
}
