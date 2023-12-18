using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtPlayerScript : MonoBehaviour
{
    private Vector3 playerPos;
    [SerializeField] private GameObject playerGameObject;

    void Start()
    {
        playerGameObject = MovementManager.instance.GetBodyObject();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerGameObject.transform.position - gameObject.transform.position;
        transform.rotation = Quaternion.LookRotation(playerPos);
    }
}
