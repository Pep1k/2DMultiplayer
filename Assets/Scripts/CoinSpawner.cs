using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    private Transform[] spawnPoint;
    public float timer;
    private float time;
    private PhotonView photonView;
    void Start()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        photonView = GetComponent<PhotonView>();
    }

    private void SpawnCoin()
    {
        int random = Random.Range(1, spawnPoint.Length);
        PhotonNetwork.Instantiate("coin", spawnPoint[random].position, Quaternion.identity);
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time>= timer)
        {
            time = 0;
            SpawnCoin();
        }

    }
}
