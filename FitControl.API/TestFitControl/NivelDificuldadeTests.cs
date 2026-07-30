using System.ComponentModel.DataAnnotations;
using FitControl.API.Entities;

namespace TestFitControl;

[TestClass]
public sealed class NivelDificuldadeValidationTests
{
    [TestMethod]
    public void Nivel_DeveTerMaximo15Caracteres()
    {
        // ARRANGE
        var nivelDificuldade = new NivelDificuldade
        {
            Nivel = "NivelComMaisDeQuinzeCaracteres"
        };

        var context = new ValidationContext(nivelDificuldade);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(nivelDificuldade, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("15 caracteres")));
    }

}