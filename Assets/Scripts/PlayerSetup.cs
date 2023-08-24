using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

    [SerializeField] private Behaviour[] componentsToDisable;
    [SerializeField]private Camera playerCam;
    // Start is called before the first frame update
    private void Start()
    {
        if (!IsOwner) {
            playerCam.gameObject.SetActive(false);
            foreach (Behaviour i in componentsToDisable)
            {
                i.enabled = false;
            }; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
