using System;
using UnityEngine;

namespace CapsuleSurvival.Core
{
    public abstract class GameParticipant : MonoBehaviour, IAppearable, IDisappearable, ISimpleVolumetric
    {
        public abstract event Action OnAppeared;
        public abstract event Action OnDisappeared;

        public abstract float Radius { get; }
        public abstract Vector3 Position { get; }

        public abstract void Appear();
        public abstract void Dissapear();

        public abstract void Launch();
        public abstract void Stop();
    }
}