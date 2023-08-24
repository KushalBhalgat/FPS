using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletPhysics : NetworkBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    void Start()
    {
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (!IsOwner) { return; }
        //else {     
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.tag == "Player") { Destroy(collision.gameObject); }
            if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "playerCam") { 
                Instantiate(hitEffectPrefab,this.transform);        
                Destroy(this.gameObject);
            }
        //}
    }
}
