using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Traps : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PayerStats>(out var stats))
        {
            var ID = stats.GetComponent<PhotonView>().ViewID;
            stats.ChangeHpGlobal(damage, ID);

        }

    }

}
