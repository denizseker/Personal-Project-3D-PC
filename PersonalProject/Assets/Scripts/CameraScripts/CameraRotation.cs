using UnityEngine;

namespace CameraControl {
	public class CameraRotation : MonoBehaviour 
	{
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _smoothing = 5f;

        private float _targetAngleY;
        private float _currentAngleY;

        private float _targetAngleX;
        private float _currentAngleX;




        private void Awake()
        {
            _targetAngleY = transform.eulerAngles.y;
            _currentAngleY = _targetAngleY;

            _targetAngleX = transform.eulerAngles.x;
            _currentAngleX = _targetAngleX;

        }

        private void HandleInput()
        {
            if (!Input.GetMouseButton(1)) return;
            _targetAngleY += Input.GetAxisRaw("Mouse X") * _speed;
            _targetAngleX += Input.GetAxisRaw("Mouse Y") * _speed;
        }

        private void Rotate()
        {
            if (_targetAngleX <= -45)
            {
                _targetAngleX = -45;
            }
            else if (_targetAngleX >= 45)
            {
                _targetAngleX = 45;
            }

            _currentAngleY = Mathf.Lerp(_currentAngleY, _targetAngleY, Time.unscaledDeltaTime * _smoothing);
            _currentAngleX = Mathf.Lerp(_currentAngleX, _targetAngleX, Time.unscaledDeltaTime * _smoothing);

            transform.rotation = Quaternion.AngleAxis(_currentAngleY, Vector3.up) * Quaternion.AngleAxis(_currentAngleX, Vector3.left);

        }



        private void Update()
        {
            HandleInput();
            Rotate();
        }


        //[SerializeField] private float _speed = 15f;
        //[SerializeField] private float _smoothing = 5f;

        //private float _targetAngle;
        //private float _currentAngle;



        //private void Awake()
        //{
        //    _targetAngle = transform.eulerAngles.y;
        //    _currentAngle = _targetAngle;

        //}

        //private void HandleInput()
        //{
        //    if (!Input.GetMouseButton(1)) return;
        //    _targetAngle += Input.GetAxisRaw("Mouse X") * _speed;
        //}

        //private void Rotate()
        //{
        //    _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, Time.deltaTime * _smoothing);
        //    Debug.Log(Quaternion.AngleAxis(_currentAngle, Vector3.up));
        //    transform.rotation = Quaternion.AngleAxis(_currentAngle, Vector3.up);

            


        //}

        //private void Update()
        //{
        //    HandleInput();
        //    Rotate();
        //}
    }
}