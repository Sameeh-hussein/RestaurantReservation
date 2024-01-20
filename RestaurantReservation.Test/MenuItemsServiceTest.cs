using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Services.Implementaion;

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

            _repositoryMock.Setup(repo => repo.CreateAsync(menuItem)).ReturnsAsync(menuItem);

            var result = await sut.CreateAsync(menuItem);

            Assert.NotNull(result);
            Assert.Equal(menuItem.menuItemId, result.menuItemId);
            Assert.Equal(menuItem.name, result.name);
            Assert.Equal(menuItem.description, result.description);
            Assert.Equal(menuItem.price, result.price);
            _repositoryMock.Verify(x => x.CreateAsync(menuItem), Times.Once);
        }

        [Fact]
        public async Task CreateMenuItem_Should_ThrowException_WhenPassNull()
        {
            MenuItems? menuItem = null; 

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.CreateAsync(menuItem));

            _repositoryMock.Verify(x => x.CreateAsync(It.IsAny<MenuItems>()), Times.Never);
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

            _repositoryMock.Setup(repo => repo.UpdateAsync(menuItem)).ReturnsAsync(newMenuItem);

            var result = await sut.UpdateAsync(menuItem);

            Assert.NotNull(result);
            Assert.Equal(newMenuItem.menuItemId, result.menuItemId);
            Assert.Equal(newMenuItem.name, result.name);
            Assert.Equal(newMenuItem.description, result.description);
            Assert.Equal(newMenuItem.price, result.price);
            _repositoryMock.Verify(x => x.UpdateAsync(menuItem), Times.Once);
        }

        [Fact]
        public async Task UpdateMenuItem_Should_ThrowException_WhenPassNull()
        {
            MenuItems? menuItem = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.UpdateAsync(menuItem));

            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<MenuItems>()), Times.Never);
        }

    }
}