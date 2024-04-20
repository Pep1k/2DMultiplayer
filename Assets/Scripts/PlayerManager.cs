using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        }
    }
}
