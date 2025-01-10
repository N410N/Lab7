using System.Collections.Generic;
using System.Linq;
using Lab7.DAL.EF;
using Lab7.DAL.EF.Entities;
using Lab7.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Lab7.DAL.Test
{
    public class OrderRepositoryTests
    {
        [Fact]
        public void GetOrdersInProgress_ReturnsOrdersForAdminWithStatusInProgress()
        {
            // Arrange
            var Orders = new List<Order>
            {
                new Order { Id = 1, AdminId = 10, Status = "In progress" },
                new Order { Id = 2, AdminId = 10, Status = "Completed" },
                new Order { Id = 3, AdminId = 10, Status = "In progress" },
                new Order { Id = 4, AdminId = 20, Status = "In progress" }
            }.AsQueryable();

            var mockDbSet = CreateMockDbSet(Orders);

            var mockContext = new Mock<Lab7Context>();
            mockContext.Setup(c => c.Set<Order>()).Returns(mockDbSet.Object);

            var repository = new OrderRepository(mockContext.Object);

            // Act
            var result = repository.getOrdersInProgress(10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, r => Assert.Equal("In progress", r.Status));
        }

        [Fact]
        public void GetOrdersInProgress_ReturnsEmptyList_WhenNoOrdersMatchCriteria()
        {
            // Arrange
            var Orders = new List<Order>
            {
                new Order { Id = 1, AdminId = 10, Status = "Completed" },
                new Order { Id = 2, AdminId = 10, Status = "Pending" }
            }.AsQueryable();

            var mockDbSet = CreateMockDbSet(Orders);

            var mockContext = new Mock<Lab7Context>();
            mockContext.Setup(c => c.Set<Order>()).Returns(mockDbSet.Object);

            var repository = new OrderRepository(mockContext.Object);

            // Act
            var result = repository.getOrdersInProgress(10);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetOrdersInProgress_ReturnsEmptyList_WhenAdminHasNoOrders()
        {
            // Arrange
            var Orders = new List<Order>
            {
                new Order { Id = 1, AdminId = 20, Status = "In progress" },
                new Order { Id = 2, AdminId = 30, Status = "Completed" }
            }.AsQueryable();

            var mockDbSet = CreateMockDbSet(Orders);

            var mockContext = new Mock<Lab7Context>();
            mockContext.Setup(c => c.Set<Order>()).Returns(mockDbSet.Object);

            var repository = new OrderRepository(mockContext.Object);

            // Act
            var result = repository.getOrdersInProgress(10);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetOrdersInProgress_IgnoresOrdersWithOtherStatuses()
        {
            // Arrange
            var Orders = new List<Order>
            {
                new Order { Id = 1, AdminId = 10, Status = "Completed" },
                new Order { Id = 2, AdminId = 10, Status = "Pending" },
                new Order { Id = 3, AdminId = 10, Status = "In progress" },
                new Order { Id = 4, AdminId = 10, Status = "In progress" }
            }.AsQueryable();

            var mockDbSet = CreateMockDbSet(Orders);

            var mockContext = new Mock<Lab7Context>();
            mockContext.Setup(c => c.Set<Order>()).Returns(mockDbSet.Object);

            var repository = new OrderRepository(mockContext.Object);

            // Act
            var result = repository.getOrdersInProgress(10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, r => Assert.Equal("In progress", r.Status));
        }

        private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockDbSet;
        }
    }
}
