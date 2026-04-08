using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFitness.Domain;
using CoreFitness.Domain.Entities;
using Assert = Xunit.Assert;
using Microsoft.AspNetCore.Identity;
using CoreFitness.Domain.Identity;


namespace CoreFitness.Tests
{
    [TestClass]
     public class UserTests
    {

        [TestMethod]
        public void CreateUser_ShouldCreateUserWithValidProperties()
        {
            // Arrange -Förberedd data
            var user = new User { FirstName = "Gabriel Ser", Email = "gabriel@test.com" };
            // Act

            // Assert
            Assert.Equal("Gabriel Ser", user.FirstName);
            Assert.Equal("gabriel@test.com", user.Email);
        }
        [TestMethod]
        public void UserMail_ShouldContainAtSymbol()
        {
            // Arrange 
            var user = new User { Email = "gabriel@test.com" };
            bool ContainsAtSymbol = user.Email.Contains("@");

            Assert.True(ContainsAtSymbol);
        }
        [TestMethod]
        public void UserAge_ShouldBeValidIfOverEighteen()
        {
            var user = new User { FirstName = "Gabriel", Age = 25 };
            int age = user.Age;

            bool isAdult = age >= 18;

            Assert.True(isAdult);
        }
    }
}