﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using BillBoardUI.Models;
using BillBoardUI.Services;
using BillBoardUI.Services.Configure;

namespace BillBoardUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        readonly NumberService _numberService;
        private string messageErrorFromService = "";

        ConfigureService _configureService = new ConfigureService();
        IConfiguration _getConfig;

        HomeModel homeModel;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _numberService = new NumberService();
            _hostingEnvironment = hostingEnvironment;
            _getConfig = _configureService.GetConfigFromAppsetting();
            homeModel = new HomeModel();
        }


        public IActionResult Index()
        {
            DataSet DSNumber = new DataSet();
            DSNumber = this._numberService.GetNumberListHomepage(ref messageErrorFromService);

            if (DSNumber != null)
            {
                if (DSNumber.Tables.Count == 0)
                {
                    ViewBag.errData = "ไม่พบข้อมูล";
                    return View();

                }
                else if (DSNumber.Tables[0].Rows.Count == 0)
                {
                    ViewBag.errData = "ไม่พบข้อมูล";
                    return View();
                }
                else
                {
                    List<NumberModel> numberList = new List<NumberModel>();
                    DataTable dtNumber = DSNumber.Tables[0];

                    foreach (DataRow dr in DSNumber.Tables[0].Rows)
                    {
                        NumberModel numberModel = new NumberModel();
                        numberModel.numberId = dr["numberId"].ToString().Trim();
                        numberModel.status = dr["status"].ToString().Trim();
                        numberModel.numberValue = int.Parse(dr["numberValue"].ToString());

                        numberList.Add(numberModel);
                    }

                    homeModel.numberList = numberList;
                    
                }

            }

            return View(homeModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
