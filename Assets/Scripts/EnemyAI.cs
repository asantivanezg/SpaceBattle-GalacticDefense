using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    [SerializeField]
    private float _speed = 5.0f;


    private float verticalLimitNegative = -7.8f;
    private float horizontalLimitNegative = -12f;
    // private float verticalLimitPositive = 7.8f;
    private float horizontalLimitPositive = 12f;

    private UiManager _uiManager;

    [SerializeField]
    private AudioClip _clip;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < verticalLimitNegative)
        {
            float randomXaxis = Random.Range(horizontalLimitNegative, horizontalLimitPositive);
            transform.position = new Vector3(randomXaxis, 7, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            if (collision.transform.parent != null)
            {
                Destroy(collision.transform.parent.gameObject);
            }
            Destroy(collision.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }


}
