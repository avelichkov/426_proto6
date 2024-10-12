using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _damping;
    private Transform _player;
    private Vector3 vel = Vector3.zero;
        
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = _player.position;
        targetPos.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, _damping);
    }
}
