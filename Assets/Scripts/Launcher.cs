using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField findInput;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        print("Conecting to server");
    }
    public override void OnConnectedToMaster()
    {
        print("�� ������������ � ������� !");
        PhotonNetwork.JoinLobby();
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions, TypedLobby.Default);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("�� ������� ������� ������� !");                                                                                                                                                                                                                                                                                                                      
    }
    public override void OnJoinedLobby()
    {
        print("�� ������������ � ���� !");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(findInput.text);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public override void OnCreatedRoom()
    {
        print("Room is created");
    }

    public override void OnJoinedRoom()
    {
        print("�� ������������ � ������� !");
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("�� ������� ������������ � �������!");
    }
}
