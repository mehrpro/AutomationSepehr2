using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AutomationSepehr.Areas.UserArea.Controllers
{
    public class UserHomeController : Controller
    {
        [Area("UserArea")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
