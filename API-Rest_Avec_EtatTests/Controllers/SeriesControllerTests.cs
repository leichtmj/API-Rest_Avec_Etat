using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_Rest_Avec_Etat.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Rest_Avec_Etat.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace API_Rest_Avec_Etat.Controllers.Tests
{
    [TestClass()]
    public class SeriesControllerTests
    {
        public SeriesDbContext _context;

        public SeriesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            _context = new SeriesDbContext(builder.Options);
        }

        [TestMethod()]
        public void SeriesControllerTest()
        {
            DbSet<Serie> test = _context.Series;

            Assert.IsNotNull(test);
        }

        [TestMethod()]
        public void GetSeriesTest()
        {

        }

        [TestMethod()]
        public void GetSerieTest()
        {

        }

        [TestMethod()]
        public void PutSerieTest()
        {

        }

        [TestMethod()]
        public void PostSerieTest()
        {

        }

        [TestMethod()]
        public void DeleteSerieTest()
        {

        }
    }
}