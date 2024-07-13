using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;



    void Start()
    {
        StartCoroutine(EnemySpawnCoroutine());
        StartCoroutine(PowerupSpawnCoroutine());
    }

    IEnumerator EnemySpawnCoroutine()
    {
        while (true)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }


    IEnumerator PowerupSpawnCoroutine()
    {
        while (true)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }

    }
}
