using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam = null;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private Vector2 zoomValues = Vector2.zero;
    private float groundDistance = 0f;

    [SerializeField] private LayerMask mask;
    private float timeInScreen = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        Ray ray = new Ray(transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~mask))
        {
            groundDistance = Vector3.Distance(transform.position, hit.point);
        }
        groundDistance = Mathf.Clamp(groundDistance, zoomValues.x, zoomValues.y);
    }

    // Update is called once per frame
    void Update()
    {
        Moving();

        Zoom();
    }

    private void Moving()
    {
        Vector3 move = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")).normalized;

        Bounds screenMin = new Bounds(new Vector3(0.5f, 0.5f), new Vector3(0.95f, 0.95f));
        Bounds screenMax = new Bounds(new Vector3(0.5f, 0.5f), new Vector3(1f, 1f));
        Vector3 viewPos = cam.ScreenToViewportPoint(Input.mousePosition);
        if (!screenMin.Contains(viewPos) && screenMax.Contains(viewPos))
        {
            timeInScreen += Time.deltaTime;
            if (timeInScreen > 0.1f)
            {
                Vector3 dir = new Vector3(viewPos.x - 0.5f, 0f, viewPos.y - 0.5f);
                move = dir.normalized;
            }
        }
        else
            timeInScreen = 0f;

        transform.position += move * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 57.5f, 942.5f), transform.position.y, Mathf.Clamp(transform.position.z, -17f, 900f));
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
