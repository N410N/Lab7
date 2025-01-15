using Lab7.BLL.DTOs;
using Lab7.BLL.Services.Implement;
using Lab7.CCL;
using Lab7.CCL.Security.Identity; // Ensure this namespace is correct
using Lab7.DAL.EF.Entities;
using Lab7.DAL.Repository.Implement;
using Lab7.DAL.Repository.Interfaces;
using Moq;
using Xunit;

namespace Lab7.BLL.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void Ctor_InputNull_ThrowArgNullException()
        {
            // Arrange
            IUnitOfWork? unitOfWork = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new OrderService(unitOfWork));
        }

        [Fact]
        public void FinishOrder_SuccessfullyFinishesOrder()
        {
            // Arrange
            var OrderId = 1;
            var OrderDto = new OrderDTO
            {
                Id = OrderId,
                AdminId = 1,
                CreatedDate = DateTime.Now,
                OrderDescription = "Problem",
                Status = "In progress",
                CustomerFeedback = "Good"
            };

            var Order = new Order
            (
                OrderDto.Id,
                OrderDto.AdminId,
                OrderDto.CreatedDate,
                OrderDto.OrderDescription, 
                OrderDto.Status,
                OrderDto.CustomerFeedback
            );

            var mockOrderRepo = new Mock<IOrderRepository>();
            mockOrderRepo.Setup(r => r.GetById(OrderDto.Id)).Returns(Order);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.GetOrderRepository()).Returns(mockOrderRepo.Object);

            var service = new OrderService(mockUnitOfWork.Object);
            var AdminUser = new AdmIdentity(1, "antony");
            SecurityContext.SetUser(AdminUser);

            // Act
            service.FinishOrder(OrderDto);

            // Assert
            mockOrderRepo.Verify(r => r.Update(It.Is<Order>(e => e.Id == OrderId && e.Status == "In progress")), Times.Once);
        }

        [Fact]
        public void FinishOrder_ThrowsException_WhenUserIsNotAdmin()
        {
            // Arrange
            var OrderDto = new OrderDTO { Id = 1 };

            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.GetOrderRepository()).Returns(mockOrderRepo.Object);

            var service = new OrderService(mockUnitOfWork.Object);
            var nonAdminUser = new EmpIdentity(1, "petro"); // Not a Admin
            SecurityContext.SetUser(nonAdminUser);

            // Act & Assert
            Assert.Throws<MethodAccessException>(() => service.FinishOrder(OrderDto));
        }

        [Fact]
        public void FinishOrder_ThrowsException_WhenOrderNotFound()
        {
            // Arrange
            var OrderId = 1;
            var OrderDto = new OrderDTO { Id = OrderId };

            var mockOrderRepo = new Mock<IOrderRepository>();
            mockOrderRepo.Setup(r => r.GetById(OrderId)).Returns((Order)null); // No Order found

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.GetOrderRepository()).Returns(mockOrderRepo.Object);

            var service = new OrderService(mockUnitOfWork.Object);
            var AdminUser = new AdmIdentity(1, "antony");
            SecurityContext.SetUser(AdminUser);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.FinishOrder(OrderDto));
        }

        [Fact]
        public void FinishOrder_ThrowsException_WhenOrderStatusIsNotInProgress()
        {
            // Arrange
            var OrderId = 1;
            var OrderDto = new OrderDTO { Id = OrderId };

            var Order = new Order
            (
                OrderDto.Id,
                OrderDto.AdminId,
                OrderDto.CreatedDate,
                OrderDto.OrderDescription,
                OrderDto.Status,
                OrderDto.CustomerFeedback
            );

            var mockOrderRepo = new Mock<IOrderRepository>();
            mockOrderRepo.Setup(r => r.GetById(OrderId)).Returns(Order);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.GetOrderRepository()).Returns(mockOrderRepo.Object);

            var service = new OrderService(mockUnitOfWork.Object);
            var AdminUser = new AdmIdentity(1, "antony");
            SecurityContext.SetUser(AdminUser);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.FinishOrder(OrderDto));
        }
    }
}
