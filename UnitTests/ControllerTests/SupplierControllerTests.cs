namespace UnitTests.ControllerTests
{
    [TestFixture]
    public class SupplierControllerTests
    {
        private Mock<ISupplierService> _mockSupplierService;
        private SuppliersController _supplierController;
        private static List<Supplier> _sampleSuppliers;

        [SetUp]
        public void SetUp()
        {
            _mockSupplierService = new Mock<ISupplierService>();
            _supplierController = new SuppliersController(_mockSupplierService.Object);

            InitialiseSampleData();
        }

        [Test]
        public async Task GetSuppliers_ReturnSuppliers_()
        {
            //Arrange
            var countOfSuppliers = _sampleSuppliers.Count;

            _mockSupplierService.Setup(service => service.GetSuppliers()).ReturnsAsync(new ServiceResponse<List<Supplier>>
            {
                Data = _sampleSuppliers,
                Success = true
            });

            //Act
            var result = await _supplierController.GetSuppliers();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var serviceResponseResult = okResult?.Value as ServiceResponse<List<Supplier>>;

            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.TypeOf<OkObjectResult>());
                Assert.That(serviceResponseResult?.Data, Is.Not.Null);
                Assert.That(serviceResponseResult?.Data?.Count, Is.EqualTo(countOfSuppliers));
            });
        }

        [Test]
        [TestCase("6e6a8fb5-8847-403a-b3cb-586d6a6c26ee")]
        [TestCase("dc23ca3f-0aeb-4fa9-8272-f309f72e662c")]
        public async Task GetSupplierById_ValidSupplierGuid_ReturnSupplier(string supplierGuidId)
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
            var result = await _supplierController.GetSupplier(supplierId);

            //Assert
            var okResult = result.Result as OkObjectResult;
            var serviceResponseResult = okResult?.Value as ServiceResponse<Supplier>;

            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.TypeOf<OkObjectResult>());
                Assert.That(serviceResponseResult?.Data, Is.Not.Null);
                Assert.That(serviceResponseResult?.Data?.Id, Is.EqualTo(supplier?.Id));
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
            var result = await _supplierController.PostSupplier(newSupplier);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var serviceResponseResult = okResult?.Value as ServiceResponse<Supplier>;

            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.TypeOf<OkObjectResult>());
                Assert.That(serviceResponseResult?.Data, Is.Not.Null);
                Assert.That(serviceResponseResult?.Data, Is.EqualTo(newSupplier));
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
            var result = await _supplierController.DeleteSupplier(supplierId);

            //Assert
            var okResult = result.Result as OkObjectResult;
            var serviceResponseResult = okResult?.Value as ServiceResponse<bool>;

            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.TypeOf<OkObjectResult>());
                Assert.That(serviceResponseResult?.Success, Is.EqualTo(true));
            });
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
