using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RemoteControl.Controllers
{
    public class CommandController : Controller
    {
        public static string LastCommand = "";

        // GET: Command
        public string getAvailable()
        {
            string Result = LastCommand;
            LastCommand = "";

            return Result;
        }

        public ActionResult Manage()
        {
            return View();
        }
    }
}