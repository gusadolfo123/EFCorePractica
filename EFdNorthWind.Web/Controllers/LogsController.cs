using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFdNorthWind.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFdNorthWind.Web.Controllers
{
    public class LogsController : Controller
    {
        readonly ILogOperations Helper;

        public LogsController(ILogOperations logOperations)
        {
            Helper = logOperations;
        }

        public IActionResult GetAll()
        {
            return View(Helper.GetAll().ToList());
        }

    }
}