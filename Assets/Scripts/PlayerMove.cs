using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rb; // Reference to the Rigidbody2D (for 2D games)
    public Weapon currentWeapon;
    public Transform firePoint;
    public int health;
    public bool isInvulnerable = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //set everything up when game restarts I'm doing it all hear for somereason
        health = 3;
        GameManager.instance.score = 0;
        GameManager.UI.UpdateScore();
        GameManager.UI.deathDisplay.SetActive(false);
        Time.timeScale = 1f;
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

    public void TakeDamage()
    {
        if (isInvulnerable) return;
        health--;
        GameManager.UI.UpdateHealth(health);
        AudioManager.instance.Play("PlayerHit");
        if (health <= 0)
        {
            GameManager.UI.deathDisplay.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        StartCoroutine(Invulnerable());
    }

    IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        int blinkNum = 6;
        float blinkTime = 0.2f;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        for (int i = 0; i < blinkNum; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(blinkTime);
            sr.enabled = true;
            yield return new WaitForSeconds(blinkTime);
        }
        isInvulnerable = false;
    }
}
