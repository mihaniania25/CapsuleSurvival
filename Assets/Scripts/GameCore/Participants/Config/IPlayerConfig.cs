namespace CapsuleSurvival.Core
{
    public interface IPlayerConfig
    {
        PlayerBase Prefab { get; }
        float BaseSpeed { get; }
    }
}