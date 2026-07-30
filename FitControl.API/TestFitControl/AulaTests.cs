using Microsoft.VisualStudio.TestTools.UnitTesting;
using FitControl.API.Entities;
using System.ComponentModel.DataAnnotations;
 
namespace TestFitControl.AulaTests;

[TestClass]
public class AulaValidationTests
{
    [TestMethod]
    public void Nome_DeveTerMaximo20Caracteres()
    {
        // ARRANGE
        var aula = new Aula
        {
            Nome = "AulaComNomeMuitoGrandeQuePassaLimite"
        };

        var context = new ValidationContext(aula);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(aula, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("20 caracteres")));
    }

    [TestMethod]
    public void Capacidade_DeveEstarEntre1e100()
    {
        // ARRANGE
        var aula = new Aula
        {
            Nome = "Yoga",
            Capacidade = 150 // inválido
        };

        var context = new ValidationContext(aula);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(aula, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("entre 1 e 100")));
    }

    [TestMethod]
    public void HoraFim_DeveSerMaiorQueHoraInicio()
    {
        // ARRANGE
        var aula = new Aula
        {
            Nome = "Pilates",
            Capacidade = 10,
            HoraInicio = DateTime.Today.AddHours(10),
            HoraFim = DateTime.Today.AddHours(9) // inválido
        };

        // ACT
        bool valido = aula.HoraFim > aula.HoraInicio;

        // ASSERT
        Assert.IsFalse(valido);
    }
}