using AutomatasGramaticalesApi.Models;

namespace AutomatasGramaticalesApi.Services
{
    public interface IValidationService
    {
        Compiler validate(string[] lines);
    }
}
