using UnityEngine;

namespace CapsuleSurvival.Utility
{
    public class CameraRectAdapter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private int _refWidth;
        [SerializeField] private int _refHeight;

        private void Awake()
        {
            if (Application.isPlaying == false)
                return;

            float refAspectRatio = (float)_refWidth / (float)_refHeight;
            float currentAspectRatio = (float)Screen.width / (float)Screen.height;

            float widthScale = refAspectRatio / currentAspectRatio;

            if (widthScale < 1.0f)
            {
                Rect newCameraRect = _camera.rect;

                newCameraRect.width = widthScale;
                newCameraRect.height = 1f;
                newCameraRect.x = (1.0f - widthScale) / 2.0f;
                newCameraRect.y = 0f;

                _camera.rect = newCameraRect;
            }
            else
            {
                float heightScale = 1.0f / widthScale;

                Rect newCameraRect = _camera.rect;

                newCameraRect.width = 1f;
                newCameraRect.height = heightScale;
                newCameraRect.x = 0f;
                newCameraRect.y = (1.0f - heightScale) / 2.0f;

                _camera.rect = newCameraRect;
            }
        }
    }
}