using Microsoft.EntityFrameworkCore;
using ProductHub.Database.Context;
using ProductHub.Database.Entities;
using ProductHub.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductHub.UnitTesting.Database
{
    public class CategoryServiceTests
    {
        [Fact]
        public async Task Create_ShouldAddCategoryToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Create_ShouldAddCategoryToDatabase")
                .Options;

            using var context = new DBContext(options);
            var categoryService = new CategoryService(context);

            var category = new Category { Name = "Test" };

            // Act
            var createdCategory = await categoryService.Create(category);

            // Assert
            Assert.NotNull(createdCategory);
            Assert.Equal(category.Name, createdCategory!.Name);
        }
        [Fact]
        public async Task GetById_ShouldReturnCategoryIfExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetById_ShouldReturnCategoryIfExists")
                .Options;

            using var context = new DBContext(options);
            var categoryService = new CategoryService(context);

            var category = new Category { Id = 1, Name = "Test" };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            // Act
            var retrievedCategory = await categoryService.GetById(1);

            // Assert
            Assert.NotNull(retrievedCategory);
            Assert.Equal(category.Name, retrievedCategory!.Name);
        }
        [Fact]
        public async Task GetAll_ShouldReturnAllCategorys()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetAll_ShouldReturnAllCategorys")
                .Options;

            using var dbContext = new DBContext(options);

            var categorys = new List<Category>
            {
                new() { Name = "Category1" },
                new() { Name = "Category2"}
                // Add more categorys if needed
            };

            dbContext.Categories.AddRange(categorys);
            dbContext.SaveChanges();

            var categoryService = new CategoryService(dbContext);

            // Act
            var result = await categoryService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categorys.Count, result.Count);
            // Add more assertions based on your requirements
        }
        [Fact]
        public async Task Update_ShouldUpdateCategory_WhenCategoryExists()
        {
            // Arrange
            var existingCategory = new Category { Name = "Category1" };
            var updatedCategory = new Category { Id = 1, Name = "UpdatedCategory" };

            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Update_ShouldUpdateCategory_WhenCategoryExists")
                .Options;

            using var dbContext = new DBContext(options);

            dbContext.Categories.Add(existingCategory);
            dbContext.SaveChanges();

            var categoryService = new CategoryService(dbContext);

            // Act
            var result = await categoryService.Update(updatedCategory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedCategory.Name, result.Name);
            // Add more assertions based on your requirements
        }

        [Fact]
        public async Task GetById_ShouldNotReturnCategoryToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetById_ShouldNotReturnCategoryToDatabase")
                .Options;

            using var context = new DBContext(options);
            var categoryService = new CategoryService(context);

            var category = new Category { Id = 1, Name = "John" };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            // Act
            var retrievedCategory = await categoryService.GetById(2);

            // Assert
            Assert.Null(retrievedCategory);
        }
        [Fact]
        public async Task GetAll_ShouldNotReturnCategoriesToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetAll_ShouldNotReturnCategoriesToDatabase")
                .Options;

            using var dbContext = new DBContext(options);

            var categoryService = new CategoryService(dbContext);

            // Act
            var result = await categoryService.GetAll();

            // Assert
            Assert.Empty(result);
            // Add more assertions based on your requirements
        }
        [Fact]
        public async Task Update_ShouldNotUpdateCategory_WhenCategoryNotExists()
        {
            // Arrange
            var existingCategory = new Category { Id = 1, Name = "Category1" };
            var updatedCategory = new Category { Id = 2, Name = "UpdatedCategory" };

            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Update_ShouldNotUpdateCategory_WhenCategoryNotExists")
                .Options;

            using var dbContext = new DBContext(options);

            dbContext.Categories.Add(existingCategory);
            dbContext.SaveChanges();

            var categoryService = new CategoryService(dbContext);

            // Act
            var result = await categoryService.Update(updatedCategory);

            // Assert
            Assert.Null(result);
            // Add more assertions based on your requirements
        }

    }
}
