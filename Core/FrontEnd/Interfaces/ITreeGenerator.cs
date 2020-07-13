namespace StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// Interface for syntax tree generator.
    /// </summary>
    public interface ITreeGenerator
    {
        ITransformationSchema BuildTransformationSchema(string vtlSource);
    }
}