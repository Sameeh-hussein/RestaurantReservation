using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Services;
using System.Collections.Generic;

namespace RestaurantReservation.Test
{
    public class MenuItemsServiceTest
    {
        private readonly Mock<IMenuItemsRepository> _repositoryMock;
        private readonly MenuItemsService sut;

        public MenuItemsServiceTest()
        {
            _repositoryMock = new Mock<IMenuItemsRepository>();
            sut = new MenuItemsService(_repositoryMock.Object);
        }

        [Theory]
        [InlineData(1, "Egg", "Perfect", 30)]
        [InlineData(2, "Milk", "Tasty", 10)]
        [InlineData(3, "tomato", "Good", 15)]
        public async Task CreateMenuItem_Should_ReturnTheItem_WhenAddTheItem(int id, string name, string description, decimal price)
        {
            var menuItem = new MenuItems
            {
                menuItemId = id,
                name = name,
                description = description,
                price = price
            };

            _repositoryMock.Setup(repo => repo.Create(menuItem)).ReturnsAsync(menuItem);

            var result = await sut.CreateMenuItem(menuItem);

            Assert.NotNull(result);
            Assert.Equal(menuItem.menuItemId, result.menuItemId);
            Assert.Equal(menuItem.name, result.name);
            Assert.Equal(menuItem.description, result.description);
            Assert.Equal(menuItem.price, result.price);
            _repositoryMock.Verify(x => x.Create(menuItem), Times.Once);
        }

        [Fact]
        public async Task CreateMenuItem_Should_ThrowException_WhenPassNull()
        {
            MenuItems menuItem = null; 

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.CreateMenuItem(menuItem));

            _repositoryMock.Verify(x => x.Create(It.IsAny<MenuItems>()), Times.Never);
        }

        [Theory]
        [InlineData(1, "Egg", "Perfect", 30, 1, "Egg", "Good", 25)]
        [InlineData(2, "Milk", "Tasty", 10, 2, "Milke", "Nice", 8)]
        [InlineData(3, "tomato", "Good", 15, 4, "Tomata", "Perfect", 12)]
        public async Task UpdateMenuItem_Should_ReturnUpdatedItem_WhenAddTheItem(int id, string name, string description, decimal price,
            int newId, string newName, string newDescription, decimal newPrice)
        {
            var menuItem = new MenuItems
            {
                menuItemId = id,
                name = name,
                description = description,
                price = price
            };

            var newMenuItem = new MenuItems
            {
                menuItemId = newId,
                name = newName,
                description = newDescription,
                price = newPrice
            };

            _repositoryMock.Setup(repo => repo.Update(menuItem)).ReturnsAsync(newMenuItem);

            var result = await sut.UpdateMenuItem(menuItem);

            Assert.NotNull(result);
            Assert.Equal(newMenuItem.menuItemId, result.menuItemId);
            Assert.Equal(newMenuItem.name, result.name);
            Assert.Equal(newMenuItem.description, result.description);
            Assert.Equal(newMenuItem.price, result.price);
            _repositoryMock.Verify(x => x.Update(menuItem), Times.Once);
        }

        [Fact]
        public async Task UpdateMenuItem_Should_ThrowException_WhenPassNull()
        {
            MenuItems menuItem = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.UpdateMenuItem(menuItem));

            _repositoryMock.Verify(x => x.Update(It.IsAny<MenuItems>()), Times.Never);
        }

    }
}