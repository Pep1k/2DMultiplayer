using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PayerStats : MonoBehaviour
{
    public int maxHp;
    public TMP_Text textHp;
    private int currentHp;
    public Image HpBar;
    private PhotonView photonView;
    private Animator animator;
    private PlayerController playerController;
    private Rigidbody2D rigidbody2;
    private Collider2D coll2d;
    private DeathPanel deathPanel;
    private SpriteRenderer spriteRenderer;
    public GameObject HpUi;
    private PlayerSpawn spawn;


    private void ChangeHpLocal(int change)
    {
        currentHp = currentHp + change;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        if (currentHp < 0)
        {
            currentHp = 0;
        }

        textHp.text = currentHp.ToString() + "/" + maxHp.ToString();
        HpBar.fillAmount = (float)currentHp / (float)maxHp;

        if (currentHp == 0)
        {
            Death();
        }
    }
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        currentHp = maxHp;
        textHp.text = currentHp.ToString() + "/" + maxHp.ToString();
        HpBar.fillAmount = (float)currentHp / (float)maxHp;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        coll2d = GetComponent<Collider2D>();
        deathPanel = FindObjectOfType<DeathPanel>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawn = FindObjectOfType<PlayerSpawn>();
    }
    public void ChangeHpGlobal(int change, int id)
    { 
        if(photonView.ViewID == id)
        {
            ChangeHpLocal(change);
        }
    }

    private void Death()
    {
        animator.Play("death");
        playerController.enabled = false;
        rigidbody2.constraints = RigidbodyConstraints2D.FreezeAll;
        coll2d.enabled = false;
        deathPanel.SetDeathPanel(true);
    }
    public void DeathEndAnimation()
    {
        spriteRenderer.enabled = false;
        HpUi.SetActive(false);
    }

    private void Respawn()
    {
        playerController.enabled = true;
        rigidbody2.constraints = RigidbodyConstraints2D.None;
        rigidbody2.constraints = RigidbodyConstraints2D.FreezeRotation;
        coll2d.enabled = true;
        deathPanel.SetDeathPanel(false);
        spriteRenderer.enabled = true;
        HpUi.SetActive(true);
        int random = Random.Range(0, spawn.transform.childCount);
        var point = spawn.transform.GetChild(random);
        transform.position = point.position;
        currentHp = maxHp;
        animator.Play("idle");
    }


    private void OnEnable()
    {
        deathPanel.OnPlayerRespawn += Respawn;

    }

    private void OnDisable()
    {
        deathPanel.OnPlayerRespawn -= Respawn;
    }



}
