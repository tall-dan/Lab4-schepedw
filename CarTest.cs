using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}
        [Test()]
        public void TestThatCarGetsCarLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            List<Int32> cars = new List<int>();
            for (var i = 1; i < 100; i++)
            {
                cars.Add(i);
                var target = new Car(i);
                target.Database = mockDatabase;
                String carLocation = target.getCarLocation(i);
                Assert.AreEqual(mockDatabase.getCarLocation(i), carLocation);
            }
        }

        [Test()]
        public void TestThatCarGetsCorrectMileage()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            List<Int32> cars = new List<int>();
            for (var i = 1; i < 100; i++)
            {
                cars.Add(i);
                var target = new Car(i);
                target.Database = mockDatabase;
                Int32 miles= target.Mileage;
                Assert.AreEqual(mockDatabase.Miles, miles);
            }
        }

        [Test()]
        public void TestObjectMother()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            var target = ObjectMother.Saab();
            target.Database = mockDatabase;
            Int32 miles = target.Mileage;
            Assert.AreEqual(mockDatabase.Miles, miles);
        }
               
	}
}
