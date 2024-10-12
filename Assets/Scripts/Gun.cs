using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform gunTransform;
    [SerializeField] private float _cooldown;
    private Camera _camera;
    private float _timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //rotating the shooting direction
        transform.rotation = Quaternion.Euler(0, 0, angle);
        _timer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && _timer < 0)
        {
            _timer = _cooldown;
            Instantiate(bullet, gunTransform.position, Quaternion.identity);
        }
    }
}
