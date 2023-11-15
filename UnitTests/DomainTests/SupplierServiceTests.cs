namespace UnitTests.DomainTests
{
    [TestFixture]
    public class SupplierServiceTests
    {
        private Mock<ISupplierService> _mockSupplierService;
        private static List<Supplier> _sampleSuppliers;

        [SetUp]
        public void SetUp()
        {
            _mockSupplierService = new Mock<ISupplierService>();

            InitialiseSampleData();
        }

        [Test]
        public async Task GetSuppliers_ReturnSuppliers()
        {
            //Arrange
            _mockSupplierService.Setup(service => service.GetSuppliers()).ReturnsAsync(new ServiceResponse<List<Supplier>>
            {
                Data = _sampleSuppliers,
                Success = true
            });

            var countOfSuppliers = _sampleSuppliers.Count;

            //Act
            var result = await _mockSupplierService.Object.GetSuppliers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Data, Has.Count.EqualTo(countOfSuppliers));
            });
        }

        [Test]
        [TestCase("6e6a8fb5-8847-403a-b3cb-586d6a6c26ee")]
        [TestCase("dc23ca3f-0aeb-4fa9-8272-f309f72e662c")]
        public async Task GetSupplier_ValidSupplierGuid_ReturnSupplier(string supplierGuidId)
        {
            //Arrange
            var supplierId = Guid.Parse(supplierGuidId);

            _mockSupplierService.Setup(service => service.GetSupplier(supplierId)).ReturnsAsync(new ServiceResponse<Supplier>
            {
                Data = _sampleSuppliers.FirstOrDefault(s => s.Id == supplierId),
                Success = true
            });

            var supplier = _sampleSuppliers.FirstOrDefault(s => s.Id == supplierId);

            //Act
            var result = await _mockSupplierService.Object.GetSupplier(supplierId);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Data?.Id, Is.EqualTo(supplier?.Id));
            });
        }

        [Test]
        public async Task AddSupplier_ValidSupplier_SuccessfullyAdd()
        {
            //Arrange
            var newSupplier = new Supplier
            {
                Id = Guid.Parse("6e6a8fb5-8847-403a-b3cb-586d6a6c26ee"),
                Title = "Mr",
                FirstName = "Larry",
                LastName = "Lobster",
                ActivationDate = DateTime.Today.AddDays(10),
                Emails = new List<Email>(),
                Phones = new List<Phone>()
            };

            _mockSupplierService.Setup(service => service.InsertSupplier(newSupplier)).ReturnsAsync(new ServiceResponse<Supplier>
            {
                Data = newSupplier,
                Success = true
            });

            //Act
            var result = await _mockSupplierService.Object.InsertSupplier(newSupplier);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Data, Is.EqualTo(newSupplier));
            });
        }

        [Test]
        public async Task DeleteSupplier_ValidSupplierGuid_SuccessfullyDelete()
        {
            //Arrange
            var supplierId = new Guid("6e6a8fb5-8847-403a-b3cb-586d6a6c26ee");

            _mockSupplierService.Setup(service => service.DeleteSupplier(supplierId)).ReturnsAsync(new ServiceResponse<bool>
            {
                Success = true
            });

            //Act
            var result = await _mockSupplierService.Object.DeleteSupplier(supplierId);

            //Assert
            Assert.That(result.Success, Is.EqualTo(true));
        }

        private static void InitialiseSampleData()
        {
            var emails = new List<Email>
            {
                new Email
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "test1@test.com",
                    IsPreferred = true
                },
                new Email
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "test2@test.com",
                    IsPreferred = false
                }
            };

            var phones = new List<Phone>
            {
                new Phone
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = "12341234",
                    IsPreferred = true
                },
                new Phone
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = "09870987",
                    IsPreferred = false
                }
            };

            _sampleSuppliers = new List<Supplier>
            {
                new Supplier
                {
                    Id = new Guid("6e6a8fb5-8847-403a-b3cb-586d6a6c26ee"),
                    FirstName = "Spongebob",
                    LastName ="Squarepants",
                    Emails = new List<Email>{emails.First() },
                    Phones =  new List<Phone>{phones.First() }
                },
                new Supplier
                {
                    Id = new Guid("dc23ca3f-0aeb-4fa9-8272-f309f72e662c"),
                    FirstName = "Patrick",
                    LastName ="Star",
                    Emails = new List<Email>{emails.Skip(1).First() },
                    Phones =  new List<Phone>{phones.Skip(1).First() }
                }
            };
        }
    }
}