using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam = null;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private Vector2 zoomValues;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;

        //if (cam.ScreenToViewportPoint(Input.mousePosition).x < 0.1f)
        //{
        //    transform.position -= Vector3.right * speed * Time.deltaTime;
        //}
        //if (cam.ScreenToViewportPoint(Input.mousePosition).x > 0.9f)
        //{
        //    transform.position += Vector3.right * speed * Time.deltaTime;
        //}
        //if (cam.ScreenToViewportPoint(Input.mousePosition).y < 0.1f)
        //{
        //    transform.position -= Vector3.forward * speed * Time.deltaTime;
        //}
        //if (cam.ScreenToViewportPoint(Input.mousePosition).y > 0.9f)
        //{
        //    transform.position += Vector3.forward * speed * Time.deltaTime;
        //}

        if ((transform.position.y > zoomValues.x && Input.mouseScrollDelta.y > 0f) ||
            (transform.position.y < zoomValues.y && Input.mouseScrollDelta.y < 0f))
        {
            transform.position += cam.transform.forward * Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, zoomValues.x, zoomValues.y), transform.position.z);
        }
    }
}
