using System;
using UnityEngine;
using CarGame.Managers;


namespace CarGame.Control
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]

    public class Controller : InputAbstract
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
        protected override void Update()
        {
            base.Update();
            AssignInput();
        }
        private void FixedUpdate()
        {
            SetMovement();
        }
        /// <summary>
        /// Resets players acceleration and sets its position and rotation accordingly.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void InitializePlayerForNextIteration(Vector3 position, Quaternion rotation)
        {
            _lerpVal = 0;
            transform.SetPositionAndRotation(position, rotation);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<ICrash>() == null)
            {
                Debug.LogError("Crashed object " + collision.gameObject + " has not implemented ICrash!");
                return;
            }
            collision.gameObject.GetComponent<ICrash>().Crash();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<ICrash>() == null)
            {
                Debug.LogError("Crashed object " + other.gameObject + " has not implemented ICrash!");
                return;
            }
            other.gameObject.GetComponent<ICrash>().Crash();
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
        private void InitialInput()
        {
            if (GameManager.instance.CurrentState == GameState.GameAwaitingStart) GameManager.instance.ChangeState(GameState.GameStarted);
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
        private void AssignInput()
        {
            switch (leftInput)
            {
                case InputState.InputDown:
                    InitialInput();
                    break;
                case InputState.InputActive:
                    LeftInputActive();
                    break;
                case InputState.InputUp:
                    DeactivateInput();
                    break;
                default:
                    break;
            }
            switch (rightInput)
            {
                case InputState.InputDown:
                    InitialInput();
                    break;
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
        #endregion
    }
}
