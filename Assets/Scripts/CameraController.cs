using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private const float zoomNear = 1.5f;
    private const float zoomFar = 15f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir.z = +1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x = -1f;

        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDir.z = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = +1f;

        }

        float speed = 5f;
        Vector3 moveVector = transform.forward * moveDir.z + transform.right * moveDir.x;
        transform.position += moveVector * speed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Vector3 rotationVector = Vector3.zero;
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = -1f;

        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = +1f;

        }

        float rotationSpeed = 50f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float zoomSpeed = 5f;
        CinemachineTransposer cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        Vector3 followOffset = cinemachineTransposer.m_FollowOffset;
        if (Input.mouseScrollDelta.y != 0)
        {
            followOffset.y += zoomSpeed * Input.mouseScrollDelta.y;
        }

        followOffset.y = Mathf.Clamp(followOffset.y, zoomNear, zoomFar);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, followOffset, zoomSpeed * Time.deltaTime);

    }
}
