namespace UnitTests.ModelTests
{
    public class SupplierExtensionsTests
    {
        [Test]
        public void IsActive_ValidActivationDate_ReturnsTrue()
        {
            //Arrange
            var supplier = new Supplier { ActivationDate = DateTime.UtcNow };

            //Act
            var isActive = supplier.IsActive();

            //Assert
            Assert.That(isActive, Is.True);
        }

        [Test]
        [TestCase("test123@testdomain.com")]
        [TestCase("test.test@anotherdomain.com")]
        public void IsValidEmail_ValidEmail_ReturnsTrue(string email)
        {
            //Arrange
            var supplier = new Supplier
            {
                Emails =
                {
                    new Email { EmailAddress = email }
                }
            };

            //Act
            var isValidEmail = supplier.IsValidEmail(out _);

            //Assert
            Assert.That(isValidEmail, Is.True);
        }

        [Test]
        [TestCase("12341234")]
        [TestCase("1234567890")]
        public void IsValidPhoneNumber_ValidPhoneNumber_ReturnsTrue(string phoneNumber)
        {
            //Arrange
            var supplier = new Supplier
            {
                Phones =
                {
                    new Phone { PhoneNumber = phoneNumber }
                }
            };

            //Act
            var isValidPhoneNumber = supplier.IsValidPhoneNumber(out _);

            // Assert
            Assert.That(isValidPhoneNumber, Is.True);
        }
    }
}
