using Microsoft.VisualStudio.TestTools.UnitTesting;
using FitControl.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace TestFitControl.InstrutorTests;

[TestClass]
public class InstrutorValidationTests
{
    [TestMethod]
    public void ContactoTelefonico_DeveSerPortuguesValido()
    {
        // ARRANGE
        var instrutor = new Instrutor
        {
            ContactoTelefonico = "123456789" // inválido
        };

        var context = new ValidationContext(instrutor);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(instrutor, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("português")));
    }

    [TestMethod]
    public void Email_DeveSerValido()
    {
        //ARRANGE
        var instrutor = new Instrutor
        {
            Email = "emailinvalido" // inválido
        };

        var context = new ValidationContext(instrutor);
        var results = new List<ValidationResult>();

        // ACT
        bool valido = Validator.TryValidateObject(instrutor, context, results, true);

        // ASSERT
        Assert.IsFalse(valido);
        Assert.IsTrue(results.Any(r => r.ErrorMessage.Contains("Email")));
    }
}