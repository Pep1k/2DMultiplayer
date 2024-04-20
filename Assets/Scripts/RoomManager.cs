using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public PlayerSpawn spawn;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        } 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {
            spawn = FindObjectOfType<PlayerSpawn>();
            int random = Random.Range(0, spawn.spawnPoinst.Length);
            PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
