using System;
using System.Linq;
using System.Threading.Tasks;
using dotnet_enterprise.Controllers;
using dotnet_enterprise.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace dotnet_enterprise_tests.Controllers
{
    [TestFixture]
    public class EventItemsControllerTests
{
    private DbContextOptions<EventContext> ContextOptions { get; }
    public EventItemsControllerTests()
    {
        ContextOptions = new DbContextOptionsBuilder<EventContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
    }

    [SetUp]
    public void Setup()
    {
        Seed();
    }

    private void Seed()
    {
        using var eventContext = new EventContext(ContextOptions);
        eventContext.Database.EnsureDeleted();
        eventContext.Database.EnsureCreated();

        var eventItem1 = new EventItem
        {
            Id = 1,
            Name = "test name 1",
            Subtitle = "test subtitle 1",
            IsFavorite = false,
            Description = "test descr 1",
            City = "test city 1",
            Location = "test location 1",
            Image = "https://pb2.jegy.hu/imgs/system-4/program/000/124/402/ahogy-teccik-original-169759.jpg",
            EventUrl = "https://www.jegy.hu/program/ahogy-teccik-124402",
            UserId = 1,
            Date = new DateTime(2021, 9, 19, 13, 00, 00),
            Category = "FESTIVAL"
        };

        var eventItem2 = new EventItem
        {
            Id = 2,
            Name = "test name 2",
            Subtitle = "test subtitle 2",
            IsFavorite = false,
            Description = "test descr 2",
            City = "test city 2",
            Location = "test location 2",
            Image = "https://pb2.jegy.hu/imgs/system-4/program/000/124/402/ahogy-teccik-original-169759.jpg",
            EventUrl = "https://www.jegy.hu/program/ahogy-teccik-124402",
            UserId = 1,
            Date = new DateTime(2021, 9, 19, 13, 00, 00),
            Category = "FESTIVAL"
        };

        eventContext.AddRange(eventItem1, eventItem2);

        eventContext.SaveChanges();
    }

    [Test]
    public async Task GetTodoItems_HasItems_ReturnAllItems()
    {
        using (var eventContext = new EventContext(ContextOptions))
        {
            var eventController = new EventItemsController(eventContext);

            var eventItemsResult = await eventController.GetEventItems();
            var eventItems = eventItemsResult.Value.ToList();
            Assert.AreEqual(2, eventItems.Count);
            Assert.AreEqual("test name 1", eventItems[0].Name);
            Assert.AreEqual("test name 2", eventItems[1].Name);
        }
    }
}
}