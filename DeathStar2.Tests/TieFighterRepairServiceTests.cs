using DeathStar2.Data.Contracts;
using DeathStar2.Model;
using DeathStar2.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DeathStar2.Tests
{
    [TestFixture]
    public class TieFighterRepairServiceTests
    {
        private readonly List<TieFighter> _data = new List<TieFighter>()
        {
            new TieFighter("ABC", true),
            new TieFighter("DEF", true),
            new TieFighter("HIJ", false)
        };

        [Test]
        public void TestRepairTieFighters()
        {
            var repo = new Mock<ITieFighterRepository>();
            repo.Setup(m => m.GetDamagedTieFighters()).Returns(() =>
            {
                return _data.Where(d => d.IsDamaged).ToList();
            });

            

            Assert.DoesNotThrow(() =>
            {
                var repairer = new TieFighterRepairService(repo.Object);
                repairer.RepairTieFighters();
            });

            // There should be no damaged tie fighters after they have been repaired.
            Assert.IsEmpty(_data.Where(d => d.IsDamaged).ToList());
        }

        [Test]
        public void TestRepairTieFighter()
        {
            var repo = new Mock<ITieFighterRepository>();
            repo.Setup(m => m.GetTieFighterByCode(It.IsAny<string>())).Returns((string code) =>
            {
                return _data.SingleOrDefault(d => d.Code == code);
            });

            Assert.DoesNotThrow(() =>
            {
                var repairer = new TieFighterRepairService(repo.Object);
                repairer.RepairTieFighterByCode("ABC");
                repairer.RepairTieFighterByCode("XYZ");
            });

            Assert.IsFalse(_data.SingleOrDefault(d => d.Code == "ABC").IsDamaged);
        }

        [Test]
        public void TestCopyTieFighter()
        {
            var repo = new Mock<ITieFighterRepository>();
            var repairer = new TieFighterRepairService(repo.Object);

            var t1 = new TieFighter()
            {
                Code = "ABC",
                IsDamaged = true
            };

            var t2 = repairer.CopyTieFighter(t1);

            Assert.AreEqual(t1.Code, t2.Code);
            Assert.AreEqual(t1.IsDamaged, t2.IsDamaged);
        }
    }
}
