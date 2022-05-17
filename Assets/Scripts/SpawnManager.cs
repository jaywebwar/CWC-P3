using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    Vector3 spawnPosition = new Vector3(25,0,0);
    private float repeatRate = 2;
    private float startDelay = 2;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnObstacle", startDelay);

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInvoking())
        {
            repeatRate = Random.Range(1.8f, 3);
            Invoke("SpawnObstacle", repeatRate);
        }
    }

    void SpawnObstacle()
    {
        if (!_playerController.gameOver)
        {
            Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
        }
    }
}
