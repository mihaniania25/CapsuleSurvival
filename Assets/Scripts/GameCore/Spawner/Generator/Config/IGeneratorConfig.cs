namespace CapsuleSurvival.Core
{
    public interface IGeneratorConfig
    {
        GenerationSettings GetSettings(GenerationType generationType);
    }
}