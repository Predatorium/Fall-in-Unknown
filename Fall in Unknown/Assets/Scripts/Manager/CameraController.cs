using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam = null;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private Vector2 zoomValues;
    private float groundDistance = 0f;

    [SerializeField] private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        groundDistance = (zoomValues.x + zoomValues.y) / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();

        Zoom();
    }

    private void Moving()
    {
        Vector3 move = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;

        //if (cam.ScreenToViewportPoint(Input.mousePosition).x < 0.1f)
        //{
        //    transform.position -= transform.right * speed * Time.deltaTime;
        //}
        //if (cam.ScreenToViewportPoint(Input.mousePosition).x > 0.9f)
        //{
        //    transform.position += transform.right * speed * Time.deltaTime;
        //}
        //if (cam.ScreenToViewportPoint(Input.mousePosition).y < 0.1f)
        //{
        //    transform.position -= transform.forward * speed * Time.deltaTime;
        //}
        //if (cam.ScreenToViewportPoint(Input.mousePosition).y > 0.9f)
        //{
        //    transform.position += transform.forward * speed * Time.deltaTime;
        //}
    }

    private void Zoom()
    {
        Ray ray = new Ray(transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~mask))
        {
            transform.position = hit.point + (transform.position - hit.point).normalized * groundDistance;
        }

        if ((groundDistance >= zoomValues.x && Input.mouseScrollDelta.y > 0f) ||
            (groundDistance <= zoomValues.y && Input.mouseScrollDelta.y < 0f))
        {
            groundDistance += -Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
            groundDistance = Mathf.Clamp(groundDistance, zoomValues.x, zoomValues.y);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, cam.transform.forward * 100);
    }
}
