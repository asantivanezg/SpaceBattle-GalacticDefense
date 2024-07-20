using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canTripleShoot = false;
    public bool isSpeedBoostActive = false;
    public bool isShieldsACtive = false;
    public int lives = 3;

    [SerializeField]
    private GameObject _playerExplosionPrefab;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject _laserTripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private float _speed = 5f;

    private float _speedMultiplier = 1.5f;

    private float verticalLimitNegative = -6.2f;
    private float horizontalLimitNegative = -11.8f;
    private float verticalLimitPositive = 6.2f;
    private float horizontalLimitPositive = 11.8f;

    private UiManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private int hitCount = 0;
    void Start()
    {
        //transform.position = new Vector3(0,0,0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();


        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }

        _audioSource = GetComponent<AudioSource>();
        hitCount = 0;
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        if (isSpeedBoostActive)
        {
            _speedMultiplier = 3.5f;
        }
        else
        {
            _speedMultiplier = 1f;
        }


        transform.Translate(x: horizontalMovement * _speed * _speedMultiplier * Time.deltaTime, y: verticalMovement * _speed * _speedMultiplier * Time.deltaTime, z: 0);

        if (transform.position.y > verticalLimitPositive)
        {
            transform.position = new Vector3(transform.position.x, verticalLimitPositive, 0);
        }
        else if (transform.position.y < verticalLimitNegative)
        {
            transform.position = new Vector3(transform.position.x, verticalLimitNegative, 0);
        }

        if (transform.position.x > horizontalLimitPositive)
        {
            transform.position = new Vector3(horizontalLimitPositive, transform.position.y, 0);
        }
        else if (transform.position.x < horizontalLimitNegative)
        {
            transform.position = new Vector3(horizontalLimitNegative, transform.position.y, 0);
        }
    }

    public void Damage()
    {

        if (isShieldsACtive == true)
        {
            isShieldsACtive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        hitCount++;

        if (hitCount == 1)
        {
            _engines[0].SetActive(true);
        }

        if (hitCount == 2)
        {
            _engines[1].SetActive(true);
        }

        lives--;

        _uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            Destroy(gameObject);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Shoot()
    {

        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShoot)
            {
                Instantiate(_laserTripleShotPrefab, transform.position, Quaternion.identity);
                //Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
                //Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, 0.1f, 0), Quaternion.identity);
                //Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, 0.1f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
            }

            _canFire = Time.time + _fireRate;
        }
    }

    public void TripleShotPowerUpOn()
    {
        canTripleShoot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void OnEnableShields()
    {
        isShieldsACtive = true;
        _shieldGameObject.SetActive(true);
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShoot = false;
    }

    public void SpeedPowerUpOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }
}
