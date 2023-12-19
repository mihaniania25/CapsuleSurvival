using System;

namespace CapsuleSurvival.Core
{
    public interface IAppearable
    {
        event Action OnAppeared;
        void Appear();
    }
}