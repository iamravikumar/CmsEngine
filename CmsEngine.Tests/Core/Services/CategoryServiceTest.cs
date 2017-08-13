﻿using CmsEngine.Data.EditModels;
using CmsEngine.Data.Models;
using CmsEngine.Data.ViewModels;
using CmsEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using CmsEngine.Tests.Fixtures;

namespace CmsEngine.Test.Core.Services
{
    public class CategoryServiceTest : IClassFixture<CategoryFixture>
    {
        private CategoryFixture categoryFixture;
        private CategoryService moqCategoryService;

        public CategoryServiceTest(CategoryFixture fixture)
        {
            categoryFixture = fixture;
            moqCategoryService = categoryFixture.Service;
        }

        #region Get

        [Fact]
        public void GetAll_ShouldReturnAllCategoriesAsQueryable()
        {
            // Arrange
            var expectedResult = categoryFixture.GetTestCategories().Count;

            // Act
            var response = moqCategoryService.GetAll();

            // Assert
            Assert.True(response is IQueryable<Category>, "Response is not IQueryable<Category>");
            Assert.Equal(expectedResult, response.Count());
        }

        [Fact]
        public void GetAllReadOnly_ShouldReturnAllCategoriesAsEnumerable()
        {
            // Arrange
            var expectedResult = categoryFixture.GetTestCategories().Count;

            // Act
            var response = (IEnumerable<CategoryViewModel>)moqCategoryService.GetAllReadOnly();

            // Assert
            Assert.True(response is IEnumerable<CategoryViewModel>, "Response is not IEnumerable<CategoryViewModel>");
            Assert.Equal(expectedResult, response.Count());
        }

        [Fact]
        public void GetById_ShouldReturnCorrectCategory()
        {
            // Arrange
            var expectedResult = categoryFixture.GetTestCategories().FirstOrDefault(q => q.Id == 2).Name;

            // Act
            var response = (CategoryViewModel)moqCategoryService.GetById(2);

            // Assert
            Assert.Equal(expectedResult, response.Name);
        }

        [Fact]
        public void GetByVanityId_ShouldReturnCorrectCategory()
        {
            // Arrange
            var expectedResult = categoryFixture.GetTestCategories().FirstOrDefault(q => q.VanityId == new Guid("8633a850-128f-4425-a2ec-30e23826b7ff")).Name;

            // Act
            var response = (CategoryViewModel)moqCategoryService.GetById(new Guid("8633a850-128f-4425-a2ec-30e23826b7ff"));

            // Assert
            Assert.Equal(expectedResult, response.Name);
        }

        #endregion

        #region Setup

        [Fact]
        public void SetupEditModel_ShouldReturnNewCategory()
        {
            // Arrange

            // Act
            var response = (CategoryEditModel)moqCategoryService.SetupEditModel();

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsNew, "Item is not new");
        }

        [Fact]
        public void SetupEditModel_ById_ShouldReturnCorrectCategory()
        {
            // Arrange
            var expectedResult = categoryFixture.GetTestCategories().FirstOrDefault(q => q.Id == 2).Name;

            // Act
            var response = (CategoryEditModel)moqCategoryService.SetupEditModel(2);

            // Assert
            Assert.IsType(typeof(CategoryEditModel), response);
            Assert.Equal(expectedResult, response.Name);
        }

        [Fact]
        public void SetupEditModel_ByVanityId_ShouldReturnCorrectCategory()
        {
            // Arrange
            var expectedResult = categoryFixture.GetTestCategories().FirstOrDefault(q => q.VanityId == new Guid("8633a850-128f-4425-a2ec-30e23826b7ff")).Name;

            // Act
            var response = (CategoryEditModel)moqCategoryService.SetupEditModel(new Guid("8633a850-128f-4425-a2ec-30e23826b7ff"));

            // Assert
            Assert.IsType(typeof(CategoryEditModel), response);
            Assert.Equal(expectedResult, response.Name);
        }

        #endregion

        #region DB Changes

        [Fact]
        public void Save_Category()
        {
            // Arrange

            // Act
            var categoryEditModel = new CategoryEditModel
            {
                Name = "Category3"
            };

            var response = moqCategoryService.Save(categoryEditModel);

            // Assert
            Assert.False(response.IsError, "Exception thrown");
        }

        [Fact]
        public void Delete_Category_By_Id()
        {
            // Arrange

            // Act
            var response = moqCategoryService.Delete(1);

            // Assert
            Assert.False(response.IsError, "Exception thrown");
        }

        [Fact]
        public void Delete_Category_By_VanityId()
        {
            // Arrange

            // Act
            var response = moqCategoryService.Delete(new Guid("8633a850-128f-4425-a2ec-30e23826b7ff"));

            // Assert
            Assert.False(response.IsError, "Exception thrown");
        }

        #endregion
    }
}