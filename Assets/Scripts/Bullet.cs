using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    public PhotonView creatorView;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] int damage;

    private PhotonView photonView;
    public void SetDiection (Vector2 dir)
    {
        direction = dir;
    }

    
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        photonView.RPC("DestroyBullet", RpcTarget.All, lifeTime);
    }

    private void Update()
    {
        transform.Translate(direction*speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PayerStats>(out var stats))
        {
            var ID = stats.GetComponent<PhotonView>().ViewID;
            if (ID == creatorView.ViewID)
            {
                return;
            }
            stats.ChangeHpGlobal(damage, ID);
        }
        PhotonNetwork.Instantiate("Impact03", transform.position, Quaternion.identity);
        photonView.RPC("DestroyBullet", RpcTarget.All, 0f);
    }
    [PunRPC]
    private void DestroyBullet(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }
}
