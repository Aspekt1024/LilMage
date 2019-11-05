using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LilMage.Networking
{
    public class QuickStartLobbyController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject cancelButton;
        [SerializeField] private TMP_InputField playerNameInput;
        [SerializeField] private int roomSize;

        private void Start()
        {
            startButton.SetActive(false);
            cancelButton.SetActive(false);
            playerNameInput.gameObject.SetActive(false);
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            startButton.SetActive(true);
            playerNameInput.gameObject.SetActive(true);
        }

        public void QuickStart()
        {
            if (playerNameInput.text == "")
            {
                return;
            }

            PlayerInfo.Instance.PlayerName = playerNameInput.text;
            startButton.SetActive(false);
            playerNameInput.gameObject.SetActive(false);
            cancelButton.SetActive(true);
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("quick start");
        }

        public void QuickCancel()
        {
            cancelButton.SetActive(false);
            startButton.SetActive(true);
            PhotonNetwork.LeaveRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to join a room");
            CreateRoom();
        }

        private void CreateRoom()
        {
            int randomRoomNumber = Random.Range(0, 10000);
            Debug.Log("Creating room now (" + randomRoomNumber + ")");
            var roomOpts = new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = (byte) roomSize,
            };

            PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOpts);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to create room... retrying");
            CreateRoom();
        }
        
    }
}