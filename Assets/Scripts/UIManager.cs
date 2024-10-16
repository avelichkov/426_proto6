using UnityEngine;
using TMPro; // Include TextMesh Pro

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI weaponText; // Reference to the TextMesh Pro UI element
    public TextMeshProUGUI ammoText;    // Reference to the ammo TextMesh Pro UI element
    public TextMeshProUGUI health;
    public TextMeshProUGUI score;
    public GameObject deathDisplay;
    private Weapon playerWeapon;         // Reference to the player's Weapon script
    private Transform player;            // Reference to the player's transform
    public Vector3 offset;               // Offset for UI elements from player position

    void Start()
    {
        // Find the player and get the Weapon component
        player = GameObject.FindWithTag("Player").transform; // Ensure player has the "Player" tag
        playerWeapon = player.GetComponentInChildren<Weapon>();

        // Ensure the texts are initially displayed
        UpdateWeaponUI();
    }

    void Update()
    {
        if (playerWeapon != null)
        {
            // Update the UI with current weapon and ammo count
            UpdateWeaponUI();
            FollowPlayer(); // Call the method to make the UI follow the player
        }
    }

    private void UpdateWeaponUI()
    {
        if (playerWeapon != null)
        {
            weaponText.text = playerWeapon.CurrentWeaponName; // Assuming you have a property for the weapon name
            SetAmmoText();
        }
    }

    public void UpdateScore()
    {
        score.text = GameManager.instance.score.ToString();
    }

    private void FollowPlayer()
    {
        // Update the position of PlayerUI to follow the player
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(player.position + offset);
        transform.position = screenPosition; // Move the UI to the player's position in screen space
    }

    private void SetAmmoText()
    {
        int bullets = playerWeapon.CurrentAmmo;
        string text = "Ammo:";
        for (int i = 0; i < bullets; i++)
        {
            text += "■";
        }
        ammoText.text = text;
    }

    public void UpdateHealth(int hp)
    {
        string newText = "";
        for (int i = 0; i < hp; i++)
        {
            newText += "♥";
        }
        health.text = newText;
    }
}
