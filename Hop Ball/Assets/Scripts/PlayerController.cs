using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] [Range(0.0001f, 0.001f)]
    private float speedGrowth = 0.0005f;
    [SerializeField] private float horizontalSpeed = 15f;
    [SerializeField] private float maxSpeed = 14f;
    [SerializeField] private float moveSpeed = 6f;

    private float _horizontalMove;
    private float _newPosX;
    private float _newPosY;
    private float _newPosZ;

    private float _startTime;
    private float _time;

    private float deltaX, deltaY;
    

    private void Start()
    {
        _startTime = Time.time;
        
    }

    private void Update()
    {
        _time = Time.time - _startTime;
        
        Moving();

        if (moveSpeed <= maxSpeed)
        {
            IncreaseSpeed();
        }
    }

    private void Moving()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, horizontalSpeed));

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    Debug.Log(touchPos.x);
                    break;
                case TouchPhase.Moved:
                    _newPosX = (touchPos.x - deltaX);
                    break;
            }
        }

       // _horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
       // 
       // _newPosX = _horizontalMove + transform.position.x;

        _newPosY = Mathf.Abs(Mathf.Sin(_time * (moveSpeed / 2))) * jumpHeight;
        
        _newPosZ = _time * moveSpeed;
        
        transform.position = new Vector3(_newPosX, _newPosY, _newPosZ);
    }
    
    private void IncreaseSpeed()
    {
        moveSpeed += speedGrowth;
        horizontalSpeed += speedGrowth;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Asd");
    }
}
