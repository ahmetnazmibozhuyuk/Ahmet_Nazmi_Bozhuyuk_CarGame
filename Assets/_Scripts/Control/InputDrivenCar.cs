using System;
using UnityEngine;
using CarGame.Managers;

namespace CarGame.Control
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]

    public class InputDrivenCar : InputAbstract,ICrash
    {
        [Header("Movement Properties")]
        [Range(0.1f, 1f)]
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

        private void FixedUpdate()
        {
            SetMovement();
        }
        private void OnEnable()
        {
            _lerpVal = 0;
        }

        #region Input - Movement Handler
        private void SetRotation()
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted)
                return;
            _rigidbody.MoveRotation(Quaternion.Euler(0, _rigidbody.transform.rotation.eulerAngles.y + _rotateValue, 0));
        }
        private void SetMovement()
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted) return;
            _lerpVal += Time.deltaTime * forwardAcceleration;
            _speedValue = Mathf.Lerp(0, maxForwardSpeed * 0.5f, _lerpVal);
            _rigidbody.MovePosition(_rigidbody.position + transform.forward * _speedValue);
        }
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
        public void AssignInput(InputInfo inputGiven)
        {
            switch (inputGiven.LeftInput)
            {
                case InputState.InputActive:
                    LeftInputActive();
                    break;
                case InputState.InputUp:
                    DeactivateInput();
                    break;
                default:
                    break;
            }
            switch (inputGiven.RightInput)
            {
                case InputState.InputActive:
                    RightInputActive();
                    break;
                case InputState.InputUp:
                    DeactivateInput();
                    break;
                default:
                    break;
            }
        }
        public void Crash()
        {
            GameManager.instance.ChangeState(GameState.GameLost);
        }
        #endregion
    }
}
