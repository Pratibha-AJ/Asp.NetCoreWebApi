using DeathStar2.Data;
using NUnit.Framework;

namespace DeathStar2.Tests
{
    [TestFixture]
    public class TieFighterRepositoryTests
    {
        [Test]
        public void TestGetDamagedTieFighters()
        {
            var repo = new TieFighterRepository();
            repo.PATH = "deathstardb.db";

            var results = repo.GetDamagedTieFighters();

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("DEF", results[0].Code);
            Assert.AreEqual("HIJ", results[1].Code);
        }

        [TestCase("ABC")]
        [TestCase("DEF")]
        [TestCase("HIJ")]
        public void TestGetTieFighter(string code)
        {
            var repo = new TieFighterRepository();
            repo.PATH = "deathstardb.db";

            var obj = repo.GetTieFighterByCode(code);
            Assert.IsNotNull(obj);
        }
    }
}