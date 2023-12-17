using UnityEngine;

namespace CapsuleSurvival.Core
{
    public interface ISimpleVolumetric
    {
        float Radius { get; }
        Vector3 Position { get; }
    }
}