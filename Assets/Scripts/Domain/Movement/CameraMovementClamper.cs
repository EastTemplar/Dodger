using UnityEngine;

namespace Dodger.Domain.Movement
{
    public class CameraMovementClamper : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private float _leftXCameraBound;
        private float _rightXCameraBound;
        private float _leftScaledXCameraBound;
        private float _rightScaledXCameraBound;

        private void GetCameraXBounds()
        {
            _leftXCameraBound = _camera.ViewportToWorldPoint(new Vector3(0, 0)).x;
            _rightXCameraBound = _camera.ViewportToWorldPoint(new Vector3(1, 0)).x;
        }

        private void GetCameraScaledXBounds()
        {
            float halfOfXScale = transform.localScale.x / 2;
            _leftScaledXCameraBound = _leftXCameraBound + halfOfXScale;
            _rightScaledXCameraBound = _rightXCameraBound - halfOfXScale;
        }

        private void Awake()
        {
            GetCameraXBounds();
            GetCameraScaledXBounds();
        }

        private void ClampPosition()
        {
            float clampedX = Mathf.Clamp(transform.position.x, _leftScaledXCameraBound, _rightScaledXCameraBound);
            transform.position = new Vector2(clampedX, transform.position.y);
        }

        private void Update()
        {
            ClampPosition();
        }
    }
}
