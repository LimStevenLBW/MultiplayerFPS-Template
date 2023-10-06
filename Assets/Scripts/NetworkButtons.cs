using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
//
public class NetworkButtons : MonoBehaviour
{
    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void StartHost()
    {
        Debug.Log("Test");
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

}
