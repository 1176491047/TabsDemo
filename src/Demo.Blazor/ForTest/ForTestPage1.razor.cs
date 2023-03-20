using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Demo.Blazor.ForTest
{

    public partial class ForTestPage1
    {
        public List<SaleInfo> dataSource = new List<SaleInfo>();
        [Inject]
        NavigationManager NavigationManager { get; set; }

        public string rootPath { get; set; }
        protected override async Task OnInitializedAsync()
        {

            rootPath = NavigationManager.BaseUri;

            List<SaleInfo> dataSource17 = new List<SaleInfo> {
        new SaleInfo {
                OrderId = 10248,
                Region = "North America",
                Country = "United States",
                City = "New York",
                Amount = 1740,
                Date = DateTime.Parse("2017/01/06")
            },
            new SaleInfo {
                OrderId = 10249,
                Region = "North America",
                Country = "United States",
                City = "Los Angeles",
                Amount = 850,
                Date = DateTime.Parse("2017/01/13")
            },
            new SaleInfo {
                OrderId = 10250,
                Region = "North America",
                Country = "United States",
                City = "Denver",
                Amount = 2235,
                Date = DateTime.Parse("2017/01/07")
            },
            new SaleInfo {
                OrderId = 10251,
                Region = "North America",
                Country = "Canada",
                City = "Vancouver",
                Amount = 1965,
                Date = DateTime.Parse("2017/01/03")
            },
            new SaleInfo {
                OrderId = 10252,
                Region = "North America",
                Country = "Canada",
                City = "Edmonton",
                Amount = 880,
                Date = DateTime.Parse("2017/01/10")
            },
            new SaleInfo {
                OrderId = 10253,
                Region = "South America",
                Country = "Brazil",
                City = "Rio de Janeiro",
                Amount = 5260,
                Date = DateTime.Parse("2017/01/17")
            },
            new SaleInfo {
                OrderId = 10254,
                Region = "South America",
                Country = "Argentina",
                City = "Buenos Aires",
                Amount = 2790,
                Date = DateTime.Parse("2017/01/21")
            },
            new SaleInfo {
                OrderId = 10255,
                Region = "South America",
                Country = "Paraguay",
                City = "Asuncion",
                Amount = 3140,
                Date = DateTime.Parse("2017/01/01")
            },
            new SaleInfo {
                OrderId = 10256,
                Region = "Europe",
                Country = "United Kingdom",
                City = "London",
                Amount = 6175,
                Date = DateTime.Parse("2017/01/24")
            },
            new SaleInfo {
                OrderId = 10257,
                Region = "Europe",
                Country = "Germany",
                City = "Berlin",
                Amount = 4575,
                Date = DateTime.Parse("2017/01/11")
            },
    };



            List<SaleInfo> dataSource18 = new List<SaleInfo> {
        new SaleInfo {
                OrderId = 10248,
                Region = "North America",
                Country = "United States",
                City = "New York",
                Amount = 1640,
                Date = DateTime.Parse("2018/01/06")
            },
            new SaleInfo {
                OrderId = 10249,
                Region = "North America",
                Country = "United States",
                City = "Los Angeles",
                Amount = 1850,
                Date = DateTime.Parse("2018/01/13")
            },
            new SaleInfo {
                OrderId = 10250,
                Region = "North America",
                Country = "United States",
                City = "Denver",
                Amount = 3235,
                Date = DateTime.Parse("2018/01/07")
            },
            new SaleInfo {
                OrderId = 10251,
                Region = "North America",
                Country = "Canada",
                City = "Vancouver",
                Amount = 965,
                Date = DateTime.Parse("2018/01/03")
            },
            new SaleInfo {
                OrderId = 10252,
                Region = "North America",
                Country = "Canada",
                City = "Edmonton",
                Amount = 1880,
                Date = DateTime.Parse("2018/01/10")
            },
            new SaleInfo {
                OrderId = 10253,
                Region = "South America",
                Country = "Brazil",
                City = "Rio de Janeiro",
                Amount = 2260,
                Date = DateTime.Parse("2018/01/17")
            },
            new SaleInfo {
                OrderId = 10254,
                Region = "South America",
                Country = "Argentina",
                City = "Buenos Aires",
                Amount = 5790,
                Date = DateTime.Parse("2018/01/21")
            },
            new SaleInfo {
                OrderId = 10255,
                Region = "South America",
                Country = "Paraguay",
                City = "Asuncion",
                Amount = 4140,
                Date = DateTime.Parse("2018/01/01")
            },
            new SaleInfo {
                OrderId = 10256,
                Region = "Europe",
                Country = "United Kingdom",
                City = "London",
                Amount = 3175,
                Date = DateTime.Parse("2018/01/24")
            },
            new SaleInfo {
                OrderId = 10257,
                Region = "Europe",
                Country = "Germany",
                City = "Berlin",
                Amount = 2101,
                Date = DateTime.Parse("2018/01/11")
            },
    };


            List<SaleInfo> dataSource19 = new List<SaleInfo> {
        new SaleInfo {
                OrderId = 10248,
                Region = "North America",
                Country = "United States",
                City = "New York",
                Amount = 1140,
                Date = DateTime.Parse("2019/01/06")
            },
            new SaleInfo {
                OrderId = 10249,
                Region = "North America",
                Country = "United States",
                City = "Los Angeles",
                Amount = 3850,
                Date = DateTime.Parse("2019/01/13")
            },
            new SaleInfo {
                OrderId = 10250,
                Region = "North America",
                Country = "United States",
                City = "Denver",
                Amount = 235,
                Date = DateTime.Parse("2019/01/07")
            },
            new SaleInfo {
                OrderId = 10251,
                Region = "North America",
                Country = "Canada",
                City = "Vancouver",
                Amount = 1965,
                Date = DateTime.Parse("2019/01/03")
            },
            new SaleInfo {
                OrderId = 10252,
                Region = "North America",
                Country = "Canada",
                City = "Edmonton",
                Amount = 2880,
                Date = DateTime.Parse("2019/01/10")
            },
            new SaleInfo {
                OrderId = 10253,
                Region = "South America",
                Country = "Brazil",
                City = "Rio de Janeiro",
                Amount = 1260,
                Date = DateTime.Parse("2019/01/17")
            },
            new SaleInfo {
                OrderId = 10254,
                Region = "South America",
                Country = "Argentina",
                City = "Buenos Aires",
                Amount = 3790,
                Date = DateTime.Parse("2019/01/21")
            },
            new SaleInfo {
                OrderId = 10255,
                Region = "South America",
                Country = "Paraguay",
                City = "Asuncion",
                Amount = 1240,
                Date = DateTime.Parse("2019/01/01")
            },
            new SaleInfo {
                OrderId = 10256,
                Region = "Europe",
                Country = "United Kingdom",
                City = "London",
                Amount = 4175,
                Date = DateTime.Parse("2019/01/24")
            },
            new SaleInfo {
                OrderId = 10257,
                Region = "Europe",
                Country = "Germany",
                City = "Berlin",
                Amount = 1101,
                Date = DateTime.Parse("2019/01/11")
            },
    };


            dataSource.AddRange(dataSource17);
            dataSource.AddRange(dataSource18);
            dataSource.AddRange(dataSource19);
        }
    }



    public class SaleInfo
    {
        public int OrderId { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
