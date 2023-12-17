using UnityEngine;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class PlaneArena : MonoBehaviour, IArena
    {
        public Vector3 GetFreeRandomPosition(ISimpleVolumetric forVolumetric)
        {
            GameLog.Error("[PlaneArena] 'GetFreeRandomPosition' not implemented");
            return Vector3.zero;
        }

        public Vector3 GetPlayerSpawnPosition()
        {
            GameLog.Error("[PlaneArena] 'GetPlayerSpawnPosition' not implemented");
            return Vector3.zero;
        }
    }
}