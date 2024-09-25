using AutoMapper;
using ChicksGold.Server.Models.Profiles;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using Models;
using Models.Domain;
using Moq;
using Services.Services;

namespace Tests
{
    public class OrderTest
    {
        private MapperConfiguration mockMapper;
        private OrderService orderService;
        private Mock<PizzaManagementDbContext> mockContext;

        public OrderTest()
        {
            var data = new List<Pizza>
            {
                new Pizza { Id = 2, Name = "Margherita", Price = 5.0m },
                new Pizza { Id = 1, Name = "Ortolana", Price = 6.0m },
                new Pizza { Id = 3, Name = "Diavola", Price = 6.5m },
                new Pizza { Id = 4, Name = "Bufalina", Price = 7.0m }
            }.AsQueryable();
            var orders = new List<Order>().AsQueryable();

            mockContext = new Mock<PizzaManagementDbContext>();

            mockContext.Setup(c => c.Pizzas).Returns(CreateDbSetMock(data.BuildMock()).Object);
            mockContext.Setup(c => c.Orders).Returns(CreateDbSetMock(orders.BuildMock()).Object);
            mockContext.Setup(c => c.OrderDetails).Returns(CreateDbSetMock(new List<OrderDetail>().AsQueryable().BuildMock()).Object);

            mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile());
            });

            orderService = new OrderService(mockContext.Object, mockMapper.CreateMapper());
        }
        [Fact]
        public async Task CheckTotal()
        {
            var result = await orderService.PlaceOrder(new OrderRequest
            {
                CustomerId = 1,
                OrderDetails = new List<OrderDetailRequest>
                {
                    new OrderDetailRequest { PizzaId = 1, Quantity = 2 },
                }
            });

            Assert.Equal(result.TotalPrice, 12);
        }

        [Fact]
        public async Task CheckNext()
        {
            mockContext.Setup(c => c.Orders).Returns(() =>
            CreateDbSetMock(new List<Order>
            {
                new Order {
                  Id = 1,
                CustomerId = 1,
                CreatedDate = DateTime.Now,
                OrderStatus = OrderStatus.Completed,
                },
                new Order {
                  Id = 2,
                CustomerId = 1,
                CreatedDate = DateTime.Now,
                OrderStatus = OrderStatus.Pending,
                },
            }.AsEnumerable().BuildMock()).Object);


            Assert.Equal(2, (await orderService.GetNextOrder()).Id);
        }


        [Fact]
        public async Task CheckPendings()
        {
            var request = new OrderRequest
            {
                CustomerId = 1,
                OrderDetails = new List<OrderDetailRequest>
                {
                    new OrderDetailRequest
                    {
                    PizzaId= 1, Quantity = 2
                }
                }
            };

            var result = await orderService.PlaceOrder(request);


            Assert.Equal(2, result.PendingOrders);
        }



        Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);

            return mockSet;

        }

    }
}