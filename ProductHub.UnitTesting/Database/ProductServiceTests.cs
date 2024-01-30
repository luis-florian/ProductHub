using Microsoft.EntityFrameworkCore;
using ProductHub.Database.Context;
using ProductHub.Database.Entities;
using ProductHub.Database.Model;
using ProductHub.Database.Services;
using Xunit;
using Image = ProductHub.Database.Entities.Image;

namespace ProductHub.UnitTesting.Database
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task Create_ShouldAddProductToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Create_ShouldAddProductToDatabase")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            // Act
            var createdProduct = await productService.Create(product);

            // Assert
            Assert.NotNull(createdProduct);
            Assert.Equal(product.Name, createdProduct!.Name);
            Assert.Equal(product.Description, createdProduct!.Description);
            Assert.Equal(product.Price, createdProduct!.Price);
            Assert.Equal(product.Stock, createdProduct!.Stock);
            Assert.Equal(product.CategoryId, createdProduct!.CategoryId);
        }
        [Fact]
        public async Task GetById_ShouldReturnProductIfExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetById_ShouldReturnProductIfExists")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            // Act
            var retrievedProduct = await productService.GetById(1);

            // Assert
            Assert.NotNull(retrievedProduct);
            Assert.Equal(product.CategoryId, retrievedProduct!.CategoryId);
            Assert.Equal(product.Name, retrievedProduct!.Name);
            Assert.Equal(product.Description, retrievedProduct!.Description);
            Assert.Equal(product.Price, retrievedProduct!.Price);
            Assert.Equal(product.Stock, retrievedProduct!.Stock);
            Assert.Equal(product.CategoryId, retrievedProduct!.CategoryId);
        }
        [Fact]
        public async Task GetAll_ShouldReturnAllProducts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetAll_ShouldReturnAllProducts")
                .Options;

            using var dbContext = new DBContext(options);

            var categoryService = new CategoryService(dbContext);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var products = new List<Product>
            {
                new() {
                        Name = "Test",
                        Description = "Testing product",
                        Price = 30,
                        Stock = 210,
                        CategoryId = 1,
                    },
                new() {
                        Name = "Test 2",
                        Description = "Testing 2 product",
                        Price = 23,
                        Stock = 240,
                        CategoryId = 1,
                    }
            };

            dbContext.Products.AddRange(products);
            dbContext.SaveChanges();

            var productService = new ProductService(dbContext);

            // Act
            var result = await productService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(products.Count, result.Count);
            // Add more assertions based on your requirements
        }
        [Fact]
        public async Task GetProductsByCategory_ShouldReturnAllProducts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetProductsByCategory_ShouldReturnAllProducts")
                .Options;

            using var dbContext = new DBContext(options);

            var categoryService = new CategoryService(dbContext);
            var category_1 = new Category { Name = "Test" };
            var category_2 = new Category { Name = "Test" };
            _ = await categoryService.Create(category_1);
            _ = await categoryService.Create(category_2);

            var products = new List<Product>
            {
                new() {
                        Name = "Test",
                        Description = "Testing product",
                        Price = 30,
                        Stock = 210,
                        CategoryId = 1,
                    },
                new() {
                        Name = "Test 2",
                        Description = "Testing 2 product",
                        Price = 23,
                        Stock = 240,
                        CategoryId = 1,
                    },
                new() {
                        Name = "Test 3",
                        Description = "Testing 3 product",
                        Price = 33,
                        Stock = 40,
                        CategoryId = 2,
                    }
            };

            dbContext.Products.AddRange(products);
            dbContext.SaveChanges();

            var productService = new ProductService(dbContext);

            // Act
            var result = await productService.GetProductsByCategory(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, product => Assert.Equal(1, product.CategoryId));
        }
        [Fact]
        public async Task Update_ShouldUpdateProduct_WhenProductExists()
        {
            // Arrange
            var existingProduct = new Product
            {
                Name = "Test 2",
                Description = "Testing 2 product",
                Price = 23,
                Stock = 240,
                CategoryId = 1,
            };
            var updatedProduct = new Product
            {
                Id = 1,
                Name = "UpdatingTest 2",
                Description = "Testing updating the product",
                Price = 23,
                Stock = 240,
                CategoryId = 1,
            };

            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Update_ShouldUpdateProduct_WhenProductExists")
                .Options;

            using var dbContext = new DBContext(options);

            var categoryService = new CategoryService(dbContext);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            dbContext.Products.Add(existingProduct);
            dbContext.SaveChanges();

            var productService = new ProductService(dbContext);

            // Act
            var result = await productService.Update(updatedProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedProduct.Name, result.Name);
        }
        [Fact]
        public async Task AddCommentToProduct_ShouldAddCommentToProduct() 
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Create_ShouldAddProductToDatabase")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var userService = new UserService(context);
            var user = new User { Name = "John", Email = "john@example.com" };
            var createdUser = await userService.Create(user);

            

            var comment = new Comment() { Content = "Test", ProductId = 1, UserId = 1  };

            // Act
            var rproduct = await productService.AddCommentToProduct(comment);

            // Assert
            Assert.NotNull(rproduct);
            Assert.True(rproduct.Comments!.Contains(comment));
        }
        [Fact]
        public async Task AddImagesToProduct_ShouldAddImageToProduct() 
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Create_ShouldAddProductToDatabase")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var images = new List<Image>()
                {
                    new() { ProductId = 1, Url = "c:\\test\\image1.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image2.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image3.jpg" },
                };

            // Act
            var productResult = await productService.AddImagesToProduct(images);

            // Assert
            Assert.NotNull(productResult);
            Assert.All(images, image => Assert.Contains(image, productResult.Images!));
        }
        [Fact]
        public async Task DeleteCommentToProduct_ShouldDeleteCommentToProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_DeleteCommentToProduct_ShouldDeleteCommentToProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var userService = new UserService(context);
            var user = new User { Name = "John", Email = "john@example.com" };
            var createdUser = await userService.Create(user);
            
            var comment = new Comment() { Content = "Test", ProductId = 1, UserId = 1 };

            _ = await productService.AddCommentToProduct(comment);

            // Act
            var rproduct = await productService.DeleteCommentToProduct(comment);

            // Assert
            Assert.NotNull(rproduct);
        }
        [Fact]
        public async Task DeleteImageToProduct_ShouldDeleteImageToProduct() 
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_DeleteImageToProduct_ShouldDeleteImageToProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var images = new List<Image>()
                {
                    new() { ProductId = 1, Url = "c:\\test\\image1.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image2.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image3.jpg" },
                };

            
            _ = await productService.AddImagesToProduct(images);

            // Act
            var productResult = await productService.DeleteImageToProduct(images[0]);

            // Assert
            Assert.NotNull(productResult);
            Assert.Equal(2, productResult.Images!.Count);
        }
        [Fact]
        public async Task GetImageFromProduct_ShouldGetImageFromProduct() 
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetImageFromProduct_ShouldGetImageFromProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var images = new List<Image>()
                {
                    new() { ProductId = 1, Url = "c:\\test\\image1.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image2.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image3.jpg" },
                };


            _ = await productService.AddImagesToProduct(images);

            // Act
            var imageResult = await productService.GetImageFromProduct(createdProduct!.Id, images[2].Id);

            // Assert
            Assert.NotNull(imageResult);
            Assert.Equal("c:\\test\\image3.jpg", imageResult.Url);
        }

        // Invalids
        [Fact]
        public async Task GetById_ShouldNotReturnProductIfExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_u")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            // Act
            var retrievedProduct = await productService.GetById(2);

            // Assert
            Assert.Null(retrievedProduct);
        }
        [Fact]
        public async Task GetAll_ShouldNotReturnAllProducts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetAll_ShouldNotReturnAllProducts")
                .Options;

            using var dbContext = new DBContext(options);

            var productService = new ProductService(dbContext);

            // Act
            var result = await productService.GetAll();

            // Assert
            Assert.Empty(result);
        }
        [Fact]
        public async Task GetProductsByCategory_ShouldNotReturnAllProducts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetProductsByCategory_ShouldNotReturnAllProducts")
                .Options;

            using var dbContext = new DBContext(options);

            var categoryService = new CategoryService(dbContext);
            var category_1 = new Category { Name = "Test" };
            var category_2 = new Category { Name = "Test" };
            _ = await categoryService.Create(category_1);
            _ = await categoryService.Create(category_2);

            var products = new List<Product>
            {
                new() {
                        Name = "Test",
                        Description = "Testing product",
                        Price = 30,
                        Stock = 210,
                        CategoryId = 1,
                    },
                new() {
                        Name = "Test 2",
                        Description = "Testing 2 product",
                        Price = 23,
                        Stock = 240,
                        CategoryId = 1,
                    },
                new() {
                        Name = "Test 3",
                        Description = "Testing 3 product",
                        Price = 33,
                        Stock = 40,
                        CategoryId = 2,
                    }
            };

            dbContext.Products.AddRange(products);
            dbContext.SaveChanges();

            var productService = new ProductService(dbContext);

            // Act
            var result = await productService.GetProductsByCategory(3);

            // Assert
            Assert.Empty(result);
        }
        [Fact]
        public async Task Update_ShouldNotUpdateProduct_WhenProductNotExists()
        {
            // Arrange
            var existingProduct = new Product
            {
                Name = "Test 2",
                Description = "Testing 2 product",
                Price = 23,
                Stock = 240,
                CategoryId = 1,
            };
            var updatedProduct = new Product
            {
                Id = 2,
                Name = "UpdatingTest 2",
                Description = "Testing updating the product",
                Price = 23,
                Stock = 240,
                CategoryId = 1,
            };

            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Update_ShouldNotUpdateProduct_WhenProductNotExists")
                .Options;

            using var dbContext = new DBContext(options);

            var categoryService = new CategoryService(dbContext);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            dbContext.Products.Add(existingProduct);
            dbContext.SaveChanges();

            var productService = new ProductService(dbContext);

            // Act
            var result = await productService.Update(updatedProduct);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteCommentToProduct_ShouldNotFoundCommentToProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_DeleteCommentToProduct_ShouldNotFoundCommentToProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var userService = new UserService(context);
            var user = new User { Name = "John", Email = "john@example.com" };
            var createdUser = await userService.Create(user);

            var comment = new Comment() { Content = "Test", ProductId = 1, UserId = 1 };

            _ = await productService.AddCommentToProduct(comment);

            var commentToFound = new Comment() { Content = "Test 2", ProductId = 1, UserId = 1, Id = 2 };

            // Act
            var rproduct = await productService.DeleteCommentToProduct(commentToFound);

            // Assert
            Assert.Null(rproduct);
        }
        [Fact]
        public async Task DeleteImageToProduct_ShouldNotFoundImageToProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_DeleteImageToProduct_ShouldNotFoundImageToProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var images = new List<Image>()
                {
                    new() { ProductId = 1, Url = "c:\\test\\image1.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image2.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image3.jpg" },
                };


            _ = await productService.AddImagesToProduct(images);

            var image = new Image() { ProductId = 1, Url = "c:\\test\\image4.jpg", Id = 4 };

            // Act
            var productResult = await productService.DeleteImageToProduct(image);

            // Assert
            Assert.Null(productResult);
        }
        [Fact]
        public async Task GetImageFromProduct_ShouldNotFoundImageFromProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetImageFromProduct_ShouldNotFoundImageFromProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var images = new List<Image>()
                {
                    new() { ProductId = 1, Url = "c:\\test\\image1.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image2.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image3.jpg" },
                };


            _ = await productService.AddImagesToProduct(images);

            // Act
            var imageResult = await productService.GetImageFromProduct(createdProduct!.Id, 4);

            // Assert
            Assert.Null(imageResult);
        }

        // Exceptions
        [Fact]
        public async Task Create_InvalidCategoryIdException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Create_InvalidCategoryIdException")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 2,
            };

            // Assert
            var exception = await Assert.ThrowsAsync<MissingRelatedEntityException>(async () => await productService.Create(product));

            // Assert that the exception message is as expected
            Assert.Equal("Category with the specified Id doesn't exist.", exception.Message);
        }
        [Fact]
        public async Task Update_InvalidCategoryIdException()
        {
            // Arrange
            var updatedProduct = new Product
            {
                Id = 2,
                Name = "UpdatingTest 2",
                Description = "Testing updating the product",
                Price = 23,
                Stock = 240,
                CategoryId = 1,
            };

            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Update_InvalidCategoryIdException")
                .Options;

            using var dbContext = new DBContext(options);

            var productService = new ProductService(dbContext);

            // Assert
            var exception = await Assert.ThrowsAsync<MissingRelatedEntityException>(async () => await productService.Create(updatedProduct));

            // Assert that the exception message is as expected
            Assert.Equal("Category with the specified Id doesn't exist.", exception.Message);
        }
        [Fact]
        public async Task AddCommentToProduct_InvalidProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_AddCommentToProduct_InvalidProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var userService = new UserService(context);
            var user = new User { Name = "John", Email = "john@example.com" };
            var createdUser = await userService.Create(user);



            var comment = new Comment() { Content = "Test", ProductId = 2, UserId = 1 };

            // Assert
            var exception = await Assert.ThrowsAsync<MissingRelatedEntityException>(async () => await productService.AddCommentToProduct(comment));

            // Assert that the exception message is as expected
            Assert.Equal("Product with the specified Id doesn't exist.", exception.Message);
        }
        [Fact]
        public async Task AddCommentToProduct_InvalidUser() 
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_AddCommentToProduct_InvalidUser")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var userService = new UserService(context);
            var user = new User { Name = "John", Email = "john@example.com" };
            var createdUser = await userService.Create(user);



            var comment = new Comment() { Content = "Test", ProductId = 1, UserId = 2 };

            // Assert
            var exception = await Assert.ThrowsAsync<MissingRelatedEntityException>(async () => await productService.AddCommentToProduct(comment));

            // Assert that the exception message is as expected
            Assert.Equal("User with the specified Id doesn't exist.", exception.Message);
        }
        [Fact]
        public async Task AddImagesToProduct_ShouldInvalidProduct() 
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_AddImagesToProduct_ShouldInvalidProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var images = new List<Image>()
                {
                    new() { ProductId = 2, Url = "c:\\test\\image1.jpg" },
                    new() { ProductId = 2, Url = "c:\\test\\image2.jpg" },
                    new() { ProductId = 2, Url = "c:\\test\\image3.jpg" },
                };

            // Assert
            var exception = await Assert.ThrowsAsync<MissingRelatedEntityException>(async () => await productService.AddImagesToProduct(images));

            // Assert that the exception message is as expected
            Assert.Equal("Product with the specified Id doesn't exist.", exception.Message);
        }
        [Fact]
        public async Task DeleteCommentToProduct_ShouldInvalidProduct() 
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_DeleteCommentToProduct_ShouldInvalidProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var userService = new UserService(context);
            var user = new User { Name = "John", Email = "john@example.com" };
            var createdUser = await userService.Create(user);

            var comment = new Comment() { Content = "Test", ProductId = 1, UserId = 1 };

            _ = await productService.AddCommentToProduct(comment);

            var commentToFound = new Comment() { Content = "Test 2", ProductId = 2, UserId = 1, Id = 1 };

            // Assert
            var exception = await Assert.ThrowsAsync<MissingRelatedEntityException>(async () => await productService.DeleteCommentToProduct(commentToFound));

            // Assert that the exception message is as expected
            Assert.Equal("Product with the specified Id doesn't exist.", exception.Message);
        }
        [Fact]
        public async Task DeleteImageToProduct_ShouldInvalidProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_DeleteImageToProduct_ShouldInvalidProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var images = new List<Image>()
                {
                    new() { ProductId = 2, Url = "c:\\test\\image1.jpg" },
                    new() { ProductId = 2, Url = "c:\\test\\image2.jpg" },
                    new() { ProductId = 2, Url = "c:\\test\\image3.jpg" },
                };

            var imageNotFound = new Image() { Id = 1, ProductId = 2, Url = "c:\\test\\image1.jpg" };

            // Assert
            var exception = await Assert.ThrowsAsync<MissingRelatedEntityException>(async () => await productService.DeleteImageToProduct(imageNotFound));

            // Assert that the exception message is as expected
            Assert.Equal("Product with the specified Id doesn't exist.", exception.Message);
        }
        [Fact]
        public async Task GetImageFromProduct_ShouldInvalidProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetImageFromProduct_ShouldInvalidProduct")
                .Options;

            using var context = new DBContext(options);
            var productService = new ProductService(context);

            var categoryService = new CategoryService(context);
            var category = new Category { Name = "Test" };
            var createdCategory = await categoryService.Create(category);

            var product = new Product
            {
                Name = "Test",
                Description = "Testing product",
                Price = 30,
                Stock = 210,
                CategoryId = 1,
            };

            var createdProduct = await productService.Create(product);

            var images = new List<Image>()
                {
                    new() { ProductId = 1, Url = "c:\\test\\image1.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image2.jpg" },
                    new() { ProductId = 1, Url = "c:\\test\\image3.jpg" },
                };


            _ = await productService.AddImagesToProduct(images);

            // Assert
            var exception = await Assert.ThrowsAsync<MissingRelatedEntityException>(async () => await productService.GetImageFromProduct(2, images[2].Id));

            // Assert that the exception message is as expected
            Assert.Equal("Product with the specified Id doesn't exist.", exception.Message);
        }
    }
}
