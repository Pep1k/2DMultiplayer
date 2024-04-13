using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private PhotonView photonView;

    [SerializeField] float lifeTime;

    [PunRPC]
    private void DestroySelf(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        photonView.RPC("DestroySelf", RpcTarget.All, lifeTime);
    }
}