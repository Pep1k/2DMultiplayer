using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    public GameObject panel;
    public event Action OnPlayerRespawn;

    public void SetDeathPanel(bool active)
    {
        panel.SetActive(active);
    }

    public void Respawn()
    {
        OnPlayerRespawn?.Invoke();
    }
}
