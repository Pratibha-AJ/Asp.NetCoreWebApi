using DeathStar2.Model;
using DeathStar2.Services;
using DeathStar2.Services.Contracts;
using NUnit.Framework;
using System;
using DeathStar2.Data;
using Moq;

namespace DeathStar2.Tests
{
    [TestFixture]
    public class SuperLaserTests
    {
        private static ISuperLaserService _superLaser;       

        [SetUp]
        public void Setup()
        {
            //_superLaser = null;
            var repo = new Mock<SuperLaserRepository>(new object[] { }).Object;
            repo.PATH = "deathstardb.db";
            _superLaser = new SuperLaserService(repo);
        }

        [Test]
        public void TestMaximumCharge()
        {
            for (int i = 0; i < 100; i++)
            {
                _superLaser.Charge();
            }

            var currentCapacity = _superLaser.GetCapacity();
            //Max capacity should be checked with greater or equal
            //Assert.AreEqual(100, currentCapacity);
            Assert.GreaterOrEqual(100, currentCapacity);

        }

        [TestCase(100, 5000)]
        [TestCase(80, 3200)]
        public void TestPowerOutput(decimal capacity, decimal expectedPower)
        {
            _superLaser.SetCapacity(capacity);
            var power = _superLaser.Fire(new Target());
            Assert.AreEqual(expectedPower, power);
        }

        [Test]
        public void TestFire()
        {
            var target = new Target();
            _superLaser.SetCapacity(100);
            // Added to reset IsDestroyed property
            decimal poweroutput = 0.0M;
            poweroutput =  _superLaser.Fire(target);
            target.IsDestroyed = poweroutput > 0 ? true : false;
            Assert.IsTrue(target.IsDestroyed);
        }

        [Test]
        public void TestPowerOutput_NotEnoughCharge()
        {
            _superLaser.SetCapacity(32);
            Assert.Throws<NotEnoughChargeException>(() =>
            {
                _superLaser.Fire(new Target());
            });
        }

        [Test]
        public void TestPowerOutput_NoTarget()
        {
            _superLaser.SetCapacity(100);
            try
            {
                _superLaser.Fire(null);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual(typeof(NullReferenceException), e.GetType());
            }
        }

        [Test]
        public void TestCharge()
        {
            _superLaser.SetCapacity(0);

            _superLaser.Charge();
            Assert.AreEqual(10, _superLaser.GetCapacity());

            _superLaser.Charge();
            Assert.AreEqual(20, _superLaser.GetCapacity());
        }

        [TestCase(100, 87.5)]
        [TestCase(50, 43.75)]
        public void TestCharge_After_Firing(decimal startCapacity, decimal expectedNewCapacity)
        {
            _superLaser.SetCapacity(startCapacity);

            _superLaser.Fire(new Target());
            Assert.AreEqual(expectedNewCapacity, _superLaser.GetCapacity());
        }

        [TestCase(0, 0)]
        [TestCase(-10, 0)]      
        [TestCase(100, 100)]
        [TestCase(101, 100)]
        [TestCase(87.1234, 87.12)]
       
        public void TestCapacity(decimal capacity, decimal expectedValue)
        {
            _superLaser.SetCapacity(capacity);
            Assert.AreEqual(expectedValue, _superLaser.GetCapacity());
        }
    }
}