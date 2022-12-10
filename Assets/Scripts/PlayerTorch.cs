using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{
    private Camera _mainCamera;
    private Light _spotLight;

    private float _xRot;
    private float _yRot;
    public float SpeedRot;

    private float _xPos;
    private float _zPos;


    private void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _spotLight = FindObjectOfType<Light>();
    }


    private void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            _xRot += Input.GetAxis("Mouse X");
            _yRot += Input.GetAxis("Mouse Y");
        }

        _xPos = Input.GetAxisRaw("Horizontal");
        _zPos = Input.GetAxisRaw("Vertical");
        MovementRot();

        if(Input.GetMouseButton(1))
        {
            RaycastHit hit;
            var mousePosition = Input.mousePosition;
            Physics.Raycast(_mainCamera.ScreenPointToRay(mousePosition), out hit);
            _spotLight.transform.LookAt(hit.point);
        }


        if(Input.GetKeyDown(KeyCode.E))
        {
            SpotActive();
        }

    }

    private void SpotActive()
    {
        if (_spotLight.gameObject.activeSelf) _spotLight.gameObject.SetActive(false);
        else _spotLight.gameObject.SetActive(true);
    }

    private void MovementRot()
    {
        Quaternion _rotPlayerCamera = Quaternion.Euler(-_yRot, _xRot*SpeedRot, 0f);
        _mainCamera.transform.rotation = _rotPlayerCamera;
        transform.rotation = Quaternion.Euler(0f, _xRot*SpeedRot, 0f);
        _spotLight.transform.rotation = _mainCamera.transform.rotation;

        var distans = new Vector3(_xPos, 0f, _zPos);
        distans = transform.TransformDirection(distans);
        transform.position += distans * 10f * Time.deltaTime;

    }
}
