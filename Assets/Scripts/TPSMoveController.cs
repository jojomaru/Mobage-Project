using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSMoveController : MonoBehaviour
{
    private VirtualStickInput vsi;
    public Transform player;
    public float moveSpeed;

    private void Start()
    {
        moveSpeed = 10f;
        vsi = GetComponent<VirtualStickInput>();
    }

    private void Update()
    {
        if (vsi.isDragging)
        {
            player.Translate(
                vsi.inputDelta.x * Time.deltaTime * moveSpeed * (Mathf.Clamp(vsi.magnitude.x, 0, 200f) / 200f),
                0f,
                vsi.inputDelta.y * Time.deltaTime * moveSpeed * (Mathf.Clamp(vsi.magnitude.y, 0, 200f) / 200f),
                Space.World);
        }
    }
}
