using UnityEngine;

namespace CapsuleSurvival.Core
{
    public interface IArena
    {
        Vector3 GetPlayerSpawnPosition();
        Vector3 GetFreeRandomPosition(ISimpleVolumetric forVolumetric);
    }
}