using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offsetY;

    [SerializeField] private float FollowSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(player.position.x, player.position.y + offsetY, -10f);
        transform.position = Vector3.Slerp(transform.position, pos, FollowSpeed * Time.deltaTime);
    }
}
