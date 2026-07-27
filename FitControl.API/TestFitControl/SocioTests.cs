using System.ComponentModel.DataAnnotations;
using FitControl.API.Entities;

namespace TestFitControl;

[TestClass]
public sealed class SocioValidationTests
{
    [TestMethod]
    public void Nome_DeveTerMaximo20Caracteres()
    {
        // ARRANGE
        var socio = new Socio
        {
            Nome = "NomeComMaisDeVinteCaracteres"
        };

        var context = new ValidationContext(socio);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(socio, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("20 caracteres")));
    }
    
    [TestMethod]
    public void Altura_DeveSerEntre1e3Metros()
    {
        // ARRANGE
        var socio = new Socio
        {
            Altura = 4 // Valor inválido para testar a validação
        };

        var context = new ValidationContext(socio);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(socio, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("4 metros")));
    }
    
    [TestMethod]
    public void Peso_DeveSerEntre35e200Kg()
    {
        // ARRANGE
        var socio = new Socio
        {
            Peso = 250 // Valor inválido para testar a validação
        };

        var context = new ValidationContext(socio);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(socio, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("250 kg")));
    }
    
    [TestMethod]
    public void ContactoTelefonico_DeveSerPortuguesValido()
    {
        // ARRANGE
        var socio = new Socio()
        {
            ContactoTelefonico = "123456789" // inválido
        };

        var context = new ValidationContext(socio);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(socio, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("português")));
    }

    [TestMethod]
    public void Email_DeveSerValido()
    {
        //ARRANGE
        var socio = new Socio
        {
            Email = "emailinvalido" // inválido
        };

        var context = new ValidationContext(socio);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(socio, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("Email")));
    }

}