﻿using CmsEngine.Data.AccessLayer;
using CmsEngine.Data.Models;
using CmsEngine.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using CmsEngine.ViewModels;
using System;

namespace CmsEngine.Test.Services
{
    [TestClass]
    public class WebsiteServiceTest
    {
        #region Get

        [TestMethod]
        public void Get_All_Websites_Queryable()
        {
            // Arrange
            var moqWebsiteService = this.SetupWebsiteService();

            // Act
            var response = moqWebsiteService.GetAll();

            // Assert
            Assert.IsTrue(response.Count() == 2, "The number of return items do not match the expected");
            Assert.IsTrue(response is IQueryable<Website>, "Response is not IQueryable<Website>");
        }

        [TestMethod]
        public void Get_All_Websites_ReadOnly()
        {
            // Arrange
            var moqWebsiteService = this.SetupWebsiteService();

            // Act
            var response = moqWebsiteService.GetAllReadOnly();

            // Assert
            Assert.IsTrue(response.Count() == 2, "The number of return items do not match the expected");
            Assert.IsTrue(response is IEnumerable<Website>, "Response is not IEnumerable<Website>");
        }

        [TestMethod]
        public void Get_Website_By_Id()
        {
            // Arrange
            var moqWebsiteService = this.SetupWebsiteService();

            // Act
            var response = moqWebsiteService.GetById(1);

            // Assert
            Assert.IsTrue(response.Name == "Website1");
        }

        [TestMethod]
        public void Get_Website_By_VanityId()
        {
            // Arrange
            var moqWebsiteService = this.SetupWebsiteService();

            // Act
            var response = moqWebsiteService.GetByVanityId(new Guid("8633a850-128f-4425-a2ec-30e23826b7ff"));

            // Assert
            Assert.IsTrue(response.Name == "Website2");
        }

        #endregion

        #region Setup

        [TestMethod]
        public void Setup_Website_ViewModel_Return_New_Item()
        {
            // Arrange
            var moqWebService = this.SetupWebsiteService();

            // Act
            var response = moqWebService.SetupViewModel();

            // Assert
            Assert.IsTrue(((BaseViewModel<Website>)response).Item != null);
        }

        [TestMethod]
        public void Setup_Website_ViewModel_Return_Multiple_Items()
        {
            // Arrange
            var moqWebService = this.SetupWebsiteService();

            // Act
            var response = moqWebService.SetupViewModel();

            // Assert
            Assert.IsTrue(((BaseViewModel<Website>)response).Items.Count() == 2);
        }

        [TestMethod]
        public void Setup_Website_ViewModel_Get_Item_By_Id()
        {
            // Arrange
            var moqWebService = this.SetupWebsiteService();

            // Act
            var response = moqWebService.SetupViewModel(2);

            // Assert
            Assert.IsTrue(((BaseViewModel<Website>)response).Item.Name == "Website2");
        }

        [TestMethod]
        public void Setup_Website_ViewModel_Get_Item_By_VanityId()
        {
            // Arrange
            var moqWebService = this.SetupWebsiteService();

            // Act
            var response = moqWebService.SetupViewModel(new Guid("8633a850-128f-4425-a2ec-30e23826b7ff"));

            // Assert
            Assert.IsTrue(((BaseViewModel<Website>)response).Item.Name == "Website2");
        }

        #endregion

        #region DB Changes

        [TestMethod]
        public void Save_Website()
        {
            // Arrange
            var moqWebService = this.SetupWebsiteService();

            // Act
            var website = new Website
            {
                Name = "Website3",
                Culture = "cs-cz",
                IsDeleted = false
            };

            var websiteViewModel = new BaseViewModel<Website>
            {
                Item = website
            };

            var response = moqWebService.Save(websiteViewModel);

            // Assert
            Assert.IsFalse(response.IsError, "Exception thrown");
        }


        [TestMethod]
        public void Delete_Website_By_Id()
        {
            // Arrange
            var moqWebService = this.SetupWebsiteService();

            // Act
            var response = moqWebService.Delete(1);

            // Assert
            Assert.IsFalse(response.IsError, "Exception thrown");
        }

        [TestMethod]
        public void Delete_Website_By_VanityId()
        {
            // Arrange
            var moqWebService = this.SetupWebsiteService();

            // Act
            var response = moqWebService.Delete(new Guid("8633a850-128f-4425-a2ec-30e23826b7ff"));

            // Assert
            Assert.IsFalse(response.IsError, "Exception thrown");
        }

        #endregion

        /// <summary>
        /// Setup the WebsiteService
        /// </summary>
        /// <returns></returns>
        private WebsiteService SetupWebsiteService()
        {
            // Create list of items
            var listWebsites = new List<Website>
            {
                new Website { Id = 1, VanityId = new Guid("278c0380-bdd2-45bb-869b-b94659bc2b89"), Name = "Website1", Culture="en-US", Description="Welcome to website 1", IsDeleted = false },
                new Website { Id = 2, VanityId = new Guid("8633a850-128f-4425-a2ec-30e23826b7ff"), Name = "Website2", Culture="pt-BR", Description="Welcome to website 2", IsDeleted = false }
            };

            // Setup the values the repository should return
            var moqRepository = new Mock<IRepository<Website>>();
            moqRepository.Setup(x => x.Get(q => q.IsDeleted == false)).Returns(listWebsites.AsQueryable());
            moqRepository.Setup(x => x.GetReadOnly(q => q.IsDeleted == false)).Returns(listWebsites);

            // Setup our unit of work
            var moqUnitOfWork = new Mock<IUnitOfWork>();
            moqUnitOfWork.Setup(x => x.GetRepository<Website>()).Returns(moqRepository.Object);

            return new WebsiteService(moqUnitOfWork.Object);
        }
    }
}
