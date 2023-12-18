using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class SkyBoxController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 2f;

        private void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotationSpeed);
        }
    }
}