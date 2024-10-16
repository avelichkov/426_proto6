using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed;
    private GameObject _player;
    private Collider2D _collider;

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = GetPosition();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private Vector2 GetPosition()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float radius = 15.0f;
        float xPos = playerPos.x + Mathf.Cos(angle) * radius;
        float yPos = playerPos.y + Mathf.Sin(angle) * radius;
        return new Vector2(xPos, yPos);
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 dir = _player.transform.position;
        dir.Normalize();
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.position = Vector2.MoveTowards(this.transform.position,
        _player.transform.position, _speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            AudioManager.instance.Play("EnemyHit");
            Debug.Log("Enemy Killed");
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            bullet.TakeDamage();
            GameManager.instance.PlusScore();
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMove>().TakeDamage();
            Debug.Log("Player Hit");
            //GameManager.instance.GameOver();
        }
    }

}
