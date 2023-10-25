using UnityEngine;


public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _smoothing = 5f;
    [HideInInspector] public bool isCameraLockedToTarget = false;

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
        if (!isCameraLockedToTarget) HandleInput();
        Rotate();
    }
}

//namespace CameraControl {
	
//}