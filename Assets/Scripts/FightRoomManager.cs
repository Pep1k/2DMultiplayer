using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FightRoomManager : MonoBehaviour
{
    public Light2D globalLight;
    public float intensity;
    void Start()
    {
        globalLight.intensity = intensity;
    }
    void Update()
    {
        
    }
}
