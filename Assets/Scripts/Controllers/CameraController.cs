using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 1000f)]
    private float _speed = 5f;

    private GameObject _player;
    private Vector3 _offsetPos;
    private float _rotX = 0, _rotY = 0;

    private void Start()
    {
        _player = PlayerController.Instance.gameObject;
        _offsetPos = _player.transform.position - transform.position;
    }

    private void Update()
    {
        LookAtMouse();
    }
    private void LateUpdate()
    {
        FollowThePlayer();
    }

    private void FollowThePlayer()
    {
        transform.position = _player.transform.position - _offsetPos;
    }
    private void LookAtMouse()
    {
        _rotX += Input.GetAxis("Mouse X") * _speed;
        _rotY += Input.GetAxis("Mouse Y") * _speed;
        transform.localRotation = Quaternion.Euler(Mathf.Clamp(-_rotY, -90f, 90f), _rotX, 0f);
    }

}
