namespace CapsuleSurvival.Core
{
    public interface IConfigsProvider
    {
        IPlayerConfig PlayerConfig { get; }
        IGeneratorConfig GeneratorConfig { get; }
    }
}