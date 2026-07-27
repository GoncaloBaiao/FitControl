using System.ComponentModel.DataAnnotations;
using FitControl.API.Entities;

namespace TestFitControl;

[TestClass]
public sealed class TipoPlanoValidationTests
{
    [TestMethod]
    public void Designacao_DeveTerMaximo15Caracteres()
    {
        // ARRANGE
        var tipoplano = new TipoPlano
        {
            Designacao = "DesignacaoComMaisDeQuinzeCaracteresDesignacaoComMaisDeQuinzeCaracteres"
        };

        var context = new ValidationContext(tipoplano);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(tipoplano, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("15 caracteres")));
    }
    
    [TestMethod]
    public void Preco_DeveSerEntre5e1000()
    {
        // ARRANGE
        var tipoplano = new TipoPlano
        {
            Preco = 1556 // Valor inválido para testar a validação
        };

        var context = new ValidationContext(tipoplano);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(tipoplano, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("5€ a 1000€")));
    }

}