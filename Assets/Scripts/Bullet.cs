using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float force;
    private Vector3 _mousePos;
    private Camera _camera;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private Transform _player;
    private int _health;

    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _player = GameObject.FindWithTag("Player").transform;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Start()
    {
        Vector3 dir = _mousePos - transform.position;
        Vector3 rot = transform.position - _mousePos;
        _rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
        float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        AudioManager.instance.Play("Shoot");
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position,_player.position) > 100)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage()
    {
        _health--;
        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //delete bullets when the get too far away
    
}
