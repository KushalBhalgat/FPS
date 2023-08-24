using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += ((transform.forward*Input.GetAxis("Vertical"))+(transform.right*Input.GetAxis("Horizontal")))*speed*Time.deltaTime;
    }
}
