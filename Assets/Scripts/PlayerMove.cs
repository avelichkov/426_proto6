using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rb; // Reference to the Rigidbody2D (for 2D games)
    public Weapon currentWeapon;
    public Transform firePoint;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        HandleShooting();
    }

    void MovePlayer()
    {
        // Get input from WASD keys
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveY = Input.GetAxis("Vertical");   // W/S or Up/Down arrows

        // Create movement vector
        Vector2 moveDirection = new Vector2(moveX, moveY);

        // Apply movement to the Rigidbody2D
        _rb.velocity = moveDirection * _speed;
    }
     void HandleShooting()
    {
        if (currentWeapon == null) return;

        if (Input.GetButtonDown("Fire1") && currentWeapon.CanShoot())
        {
            currentWeapon.Shoot();
        }
    }

    public void UpgradeWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        
    }
}
