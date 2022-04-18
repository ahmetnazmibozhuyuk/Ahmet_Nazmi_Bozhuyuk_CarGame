using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using CarGame.Managers;


namespace CarGame
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] private ControllersList controllersList;

        [Header("Movement Properties")]
        [Range(0.05f,0.4f)]
        [SerializeField] private float maxForwardSpeed;
        [Tooltip("Determines how fast the vehicle will accelerate upon start.")]
        [SerializeField] private float forwardAcceleration;
        [Tooltip("Determines how sharp the steering is done.")]
        [SerializeField] private float maxSteer;

        private float _lerpVal = 0;

        private float _rotateValue;
        private float _speedValue;

        private Rigidbody _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Start()
        {

        }
        private void Update()
        {
            //if (GameManager.instance.currentState != GameState.GameStarted) return;
            AssignInput();
            SetMovement();

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<ICrash>() == null)
            {
                Debug.LogError("Crashed object has not implemented ICrash!");
                return;
            }
            collision.gameObject.GetComponent<ICrash>().Crash();
        }
        private void SetMovement()
        {
            _lerpVal += Time.deltaTime * forwardAcceleration;
            _speedValue = Mathf.Lerp(0, maxForwardSpeed, _lerpVal);
            _rigidbody.MovePosition(_rigidbody.position+transform.forward*_speedValue);
        }
        private void FixedUpdate()
        {

        }
        private void SetRotation()
        {
            _rigidbody.MoveRotation(Quaternion.Euler(0, _rigidbody.transform.rotation.eulerAngles.y + _rotateValue, 0));
        }
        #region Input Handler
        private void LeftInputActive()
        {
            _rotateValue = -maxSteer;
            SetRotation();
        }
        private void RightInputActive()
        {
            _rotateValue = maxSteer;
            SetRotation();
        }
        private void DeactivateInput()
        {
            _rotateValue = 0;
        }
        private void TouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    DeactivateInput();
                    return;
                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        LeftInputActive();
                    }
                    else if (touch.position.x > Screen.width / 2)
                    {
                        RightInputActive();
                    }
                }
            }
        }
        private void MouseInput()
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    LeftInputActive();
                }
                else if (Input.mousePosition.x > Screen.width / 2)
                {
                    RightInputActive();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                DeactivateInput();
            }
        }
        private void AssignInput()
        {
            switch (controllersList)
            {
                case ControllersList.MouseController:
                    MouseInput();
                    break;
                case ControllersList.TouchController:
                    TouchInput();
                    break;
                default:
                    throw new ArgumentException("Invalid controller selection.");
            }
        }
        #endregion
    }
    public enum ControllersList
    {
        MouseController = 0, TouchController = 1
    }
}
