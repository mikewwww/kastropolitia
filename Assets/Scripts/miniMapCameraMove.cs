using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class miniMapCameraMove : MonoBehaviour
{
    public Transform player;
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y =transform.position.y;
        transform.position = newPosition;
    }
}
