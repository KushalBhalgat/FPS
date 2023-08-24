using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkUI : NetworkBehaviour
{
    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;
    [SerializeField] private Button ServerButton;
    private void Awake()
    {
        HostButton.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        ClientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
        ServerButton.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
