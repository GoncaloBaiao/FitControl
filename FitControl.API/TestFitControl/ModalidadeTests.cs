using System.ComponentModel.DataAnnotations;
using FitControl.API.Entities;

namespace TestFitControl;

[TestClass]
public sealed class ModalidadeValidationTests
{
    [TestMethod]
    public void Descricao_DeveTerMaximo100Caracteres()
    {
        // ARRANGE
        var modalidade = new Modalidade
        {
            Descricao = "DescricaoComMaisDeCemCaracteresDescricaoComMaisDeCemCaracteresDescricaoComMaisDeCemCaracteresDescricaoComMaisDeCemCaracteres"
        };
 
        var context = new ValidationContext(modalidade);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(modalidade, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("100 caracteres")));
    }

}