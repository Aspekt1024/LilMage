using System;
using System.IO;
using Photon.Pun;
using UnityEngine;

namespace LilMage.Networking
{
    public class NetworkedGameSetupController : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        
        private void Start()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            Debug.Log("Creating Player");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnPoint.position, Quaternion.identity);
        }
    }
}