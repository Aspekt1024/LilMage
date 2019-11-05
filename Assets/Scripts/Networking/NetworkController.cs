using System;
using Photon.Pun;
using UnityEngine;

namespace LilMage.Networking
{
    public class NetworkController : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN connected to " + PhotonNetwork.CloudRegion);
        }
    }
}