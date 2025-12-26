using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.IO;
using Tyuiu.KoryakinaAE.Sprint7.Project.V12.Lib;
namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12.Test
{
    [TestClass]
    public sealed class DataServiceTest
    {
        private DataService service;

        [TestInitialize]
        public void Init()
        {
            service = new DataService();
        }

        [TestMethod]
        public void LoadCsv_ValidCsv_LoadsCorrectly()
        {
            string path = "pc_test.csv";
            File.WriteAllText(path,
                "Фирма;Тактовая частота;RAM;HDD\n" +
                "Dell;3.5;16;1000\n" +
                "HP;3.2;8;500");

            var table = service.LoadCsv(path);

            Assert.AreEqual(4, table.Columns.Count);
            Assert.AreEqual(2, table.Rows.Count);
            Assert.AreEqual("Dell", table.Rows[0]["Фирма"]);
            Assert.AreEqual("500", table.Rows[1]["HDD"]);

            File.Delete(path);
        }

        [TestMethod]
        public void SaveCsv_CreatesFile()
        {
            var table = new DataTable();
            table.Columns.Add("Фирма");
            table.Columns.Add("RAM");

            table.Rows.Add("Lenovo", "8");

            string path = "pc_save.csv";

            service.SaveCsv(table, path);

            Assert.IsTrue(File.Exists(path));
            var content = File.ReadAllText(path);
            Assert.IsTrue(content.Contains("Lenovo"));

            File.Delete(path);
        }

        [TestMethod]
        public void GetColumnStatistics_ReturnsCorrectValues()
        {
            var table = new DataTable();
            table.Columns.Add("RAM");

            table.Rows.Add("8");
            table.Rows.Add("16");
            table.Rows.Add("32");

            var stats = service.GetColumnStatistics(table, "RAM");

            Assert.AreEqual(3, stats.Count);
            Assert.AreEqual(8, stats.Min);
            Assert.AreEqual(32, stats.Max);
            Assert.AreEqual(18.67, stats.Average);
        }

        [TestMethod]
        public void GetColumnStatistics_EmptyTable_ReturnsZeros()
        {
            var table = new DataTable();
            table.Columns.Add("HDD");

            var stats = service.GetColumnStatistics(table, "HDD");

            Assert.AreEqual(0, stats.Count);
            Assert.AreEqual(0, stats.Min);
            Assert.AreEqual(0, stats.Max);
            Assert.AreEqual(0, stats.Average);
        }
    }
}