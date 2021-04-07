using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }

    [SerializeField]
    [Range(1f, 1000f)]
    private float _speed = 5f;
    private Camera _mainCam;
    private Rigidbody _rb;

    private PlayerManager _playerManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        _mainCam = FindObjectOfType<CameraController>().GetComponent<Camera>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _playerManager = PlayerManager.Instance;
    }

    private void Update()
    {
        RotateAndForce();
    }
    private void RotateAndForce()
    {
        //Rotate (follow Camera)
        transform.eulerAngles = new Vector3(0, _mainCam.transform.eulerAngles.y, 0);

        //RB add Force
        Vector3 forceVector = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * Time.deltaTime * _speed;
        _rb.AddForce(forceVector - _rb.velocity, ForceMode.VelocityChange);
    }

    public void TakePistol(GameObject pistol)
    {
        pistol.GetComponent<PistolController>().IsActive = true;
        Transform hand = _playerManager.Hand;
        pistol.transform.SetParent(hand);
        pistol.transform.localPosition = Vector3.zero;
        pistol.transform.localRotation = Quaternion.identity;

        //Enable Pistol UI
        CanvasManager.Instance.PistolUIManager.gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<TableController>())
        {
            TableController tableController = collision.gameObject.GetComponent<TableController>();
            tableController.IsActive = true;
            tableController.TableActionItemMsg();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Table" || collision.gameObject.tag == "Door")
        {
            CanvasManager.Instance?.ClearTextMsg();
        }
    }
}
