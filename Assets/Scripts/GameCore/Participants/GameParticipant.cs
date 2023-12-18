using System;
using UnityEngine;

namespace CapsuleSurvival.Core
{
    public abstract class GameParticipant : MonoBehaviour, IAppearable, IDisappearable, ISimpleVolumetric
    {
        public abstract event Action OnAppeared;
        public abstract event Action OnDisappeared;

        public abstract float Radius { get; protected set; }
        public virtual Vector3 Position => transform.position;

        public abstract void Setup();
        public abstract void Launch();
        public abstract void Stop();

        public abstract void Appear();
        public abstract void Dissapear();
    }
}