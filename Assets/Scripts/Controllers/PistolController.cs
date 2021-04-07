using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    private PistolManager _pistolManager;
    private PistolUIManager _pistolUIManager;
    public virtual event Action Shoot = null;

    private GameManager _gm;
    [SerializeField]
    [Range(1, 15)]
    private float _rechangeTime = 3;
    [SerializeField]
    [Range(20, 50)]
    private int _bulletsCountInClip = 20;
    private int _bulletsCount = 0;

    [SerializeField]
    [Range(10, 50)]
    private int _playerBulletsLimit = 10;
    [SerializeField]
    [Range(0f, 3f)]
    private float _bulletSpawnTime = 1f;
    private float? _timeRespawn = 0f;
    private bool _isSpawned = false;
    private bool _isRechange = false;
    public bool IsActive = false;

    private void Awake()
    {
        Shoot += PistolShoot;
    }
    private void Start()
    {
        _gm = GameManager.Instance;
        _pistolManager = GetComponent<PistolManager>();
        _pistolUIManager = CanvasManager.Instance.PistolUIManager;

        StartCoroutine(Rechange());
    }
    private void Update()
    {
        if (IsActive)
        {
            if (!_isSpawned && Input.GetMouseButtonDown(0)
    && _gm.SpawnBullets.Count < _playerBulletsLimit
    && !_isRechange) Shoot?.Invoke();
            Timer();
        }
    }

    private void Timer()
    {
        _timeRespawn += Time.deltaTime;
        if (_timeRespawn > _bulletSpawnTime)
        {
            _timeRespawn = 0f;
            if (_isSpawned) _isSpawned = false;
        }
    }

    private void PistolShoot()
    {
        _bulletsCount--;
        Spawn(_pistolManager.BulletSpawnPoint.transform.position, transform.forward);
        _pistolUIManager.BulletsCountText.text = $"{_bulletsCount}/{_bulletsCountInClip}";
        if(_bulletsCount == 0)
        {
            StartCoroutine(Rechange());
        }
    }
    private IEnumerator Rechange()
    {
        _isRechange = true;

        float rechangeTime = _rechangeTime;
        _pistolUIManager.BulletsCountText.text = "Rechange...";
        yield return new WaitForSeconds(rechangeTime / 3);
        rechangeTime = (rechangeTime-rechangeTime / 3) / _bulletsCountInClip;
        for (int i = 0;i< _bulletsCountInClip; i++)
        {
            yield return new WaitForSeconds(rechangeTime);
            _bulletsCount++;
            _pistolUIManager.BulletsCountText.text = $"{_bulletsCount}/{_bulletsCountInClip}";
        }

        _isRechange = false;
    }
    public void Spawn(Vector3 spawnPoint, Vector3 forwardVector)
    {
        _isSpawned = true;
        BulletController bullet = Instantiate(_pistolManager.BulletPrefab, _pistolManager.BulletsParent.transform).GetComponent<BulletController>();
        _gm.SpawnBullets.Add(bullet);

        bullet.Init(spawnPoint, forwardVector);
    }

}
