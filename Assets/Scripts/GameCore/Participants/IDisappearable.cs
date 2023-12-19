using System;

namespace CapsuleSurvival.Core
{
    public interface IDisappearable
    {
        event Action OnDisappeared;
        void Dissapear();
    }
}