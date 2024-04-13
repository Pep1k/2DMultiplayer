using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private PhotonView photonView;


    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    private void DestroySelf(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PayerStats>(out var stats))
        {
            photonView.RPC("DestroySelf", RpcTarget.All, 0f);
        }
    }
}
