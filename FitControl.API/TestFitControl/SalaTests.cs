using System.ComponentModel.DataAnnotations;
using FitControl.API.Entities;

namespace TestFitControl;

[TestClass]
public sealed class SalaValidationTests
{
    [TestMethod]
    public void Nome_DeveTerMaximo20Caracteres()
    {
        // ARRANGE
        var sala = new Sala
        {
            Nome = "NomeComMaisDeVinteCaracteres"
        };

        var context = new ValidationContext(sala);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(sala, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("20 caracteres")));
    }
    
    [TestMethod]
    public void Descricao_DeveTerMaximo100Caracteres()
    {
        // ARRANGE
        var sala = new Sala
        {
            Descricao = "DescricaoComMaisDeCemCaracteresDescricaoComMaisDeCemCaracteresDescricaoComMaisDeCemCaracteresDescricaoComMaisDeCemCaracteresDescricaoComMaisDeCemCaracteres"
        };

        var context = new ValidationContext(sala);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(sala, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("100 caracteres")));
    }

}