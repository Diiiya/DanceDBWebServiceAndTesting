using DanceDLL;
using DanceWebServiceDBAndTesting.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DanceUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private readonly DancesController _controller = new DancesController();

        [TestMethod]
        public void TestGetAllMethod()
        {
            IEnumerable<Dance> dancesList = _controller.Get();
            Assert.AreEqual(3, dancesList.Count());

            Dance dance = _controller.Get(8);
            Assert.AreEqual("Bachata", dance.DName);

            dance = _controller.Get(1);
            Assert.IsNull(dance);
        }

        [TestMethod]
        public void TestDeleteMethod()
        {
            int dlistCount = _controller.Delete(1);
            Assert.AreEqual(0, dlistCount);

            dlistCount = _controller.Delete(7);
            Assert.AreEqual(0, dlistCount);

            IEnumerable<Dance> danceList = _controller.Get();
            Assert.AreEqual(3, danceList.Count());
        }

        [TestMethod]
        public void TestPostMethod()
        {
            Dance newDance = new Dance
            {
                DName = "Waltz",
                DDescription = "A slow partner dance.",
                Photo = "",
                Country = "Austria",
                TimeAppeared = 1580,
                DType = "Ballroom"
            };
            int dancesCount = _controller.Post(newDance);
            Assert.AreEqual(1, dancesCount);

            IEnumerable<Dance> dancesList = _controller.Get();
            Assert.AreEqual(4, dancesList.Count());
        }

        [TestMethod]
        public void TestPutMethod()
        {
            Dance updatedDance = new Dance
            {
                DName = "Waltz",
                DDescription = "A slow dance, perfomrmed normally in closed position.",
                Photo = "",
                Country = "Austria",
                TimeAppeared = 1600,
                DType = "Ballroom"
            };
            int dancesCount = _controller.Put(11, updatedDance);
            Assert.AreEqual(1600, updatedDance.TimeAppeared);
        }
    }


}
