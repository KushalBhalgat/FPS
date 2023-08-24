//using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerShooter : NetworkBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject impactEffectPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletDirection;
    private Vector3 rayCastDirection;
    private Vector3 gunOffset;
    private Vector3 alterCamPosition;
    private float shootCooldownTimer;
    private float shootCooldownConst;
    [SerializeField]private Transform bulletSpawner;
    private Vector3 bulletMovingDirection;
    private float bulletVelocity;

    void Start()
    {
        if (!IsOwner) {return;}
        bulletVelocity = 150f;
        //bulletSpawner = GameObject.FindWithTag("BulletSpawner").GetComponent<Transform>();
        shootCooldownConst =0.15f;
        shootCooldownTimer = 0f;
        alterCamPosition = playerCam.transform.position;
        alterCamPosition.y = gun.transform.position.y;
        gunOffset = (playerCam.transform.position.x-gun.transform.position.x)*playerCam.transform.right;
        rayCastDirection=playerCam.transform.TransformDirection(transform.forward)-gunOffset;
    }

    void Update()
    {
        if (!IsOwner) { return; }
        shootCooldownTimer += Time.deltaTime;
        rayCastDirection = bulletDirection.forward + bulletDirection.position - gun.transform.position;
        RaycastHit hit;
        if (Input.GetButton("Fire1") && shootCooldownTimer>shootCooldownConst && Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, Mathf.Infinity))
        {

            shootCooldownTimer = 0f;
            GameObject temp=Instantiate(bulletPrefab, bulletSpawner.position,Quaternion.LookRotation(playerCam.transform.forward));
            temp.GetComponent<Rigidbody>().useGravity = false;
            temp.GetComponent<Rigidbody>().velocity = (hit.point -bulletSpawner.position).normalized*bulletVelocity;
            Debug.DrawRay(gun.transform.position, (hit.point - bulletSpawner.position).normalized * hit.distance,Color.cyan);
            Debug.Log(hit.collider.name);
            Destroy(temp, 1f);
        } 
    }
}
