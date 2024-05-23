using Microsoft.AspNetCore.Mvc;
using Pscbag.Models;
using Pscbag.Services;
using System.Diagnostics;

namespace Pscbag.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AccountService accountService;

        public HomeController(ILogger<HomeController> logger, AccountService _accountService)
        {
            _logger = logger;
            accountService = _accountService;
        }

        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Check")]
        public IActionResult Check()
        {
            return View();
        }
        [Route("Check_date")]
        public IActionResult Check_date()
        {
            return View();
        }
        [Route("Login_bao")]
        public IActionResult Login_bao()
        {
            return View();
        }

        [Route("HandBag")]
        public IActionResult HandBag()
        {
            return View();
        }

        [Route("StripedBag")]
        public IActionResult StripedBag()
        {
            return View();
        }

        [Route("WhitedBag")]
        public IActionResult WhitedBag()
        {
            return View();
        }

        [Route("OtherBag")]
        public IActionResult OtherBag()
        {
            return View();
        }

        [Route("InnerBag")]
        public IActionResult InnerBag()
        {
            return View();
        }

        [Route("ThreadingBag")]
        public IActionResult ThreadingBag()
        {
            return View();
        }

        [Route("HangingBag")]
        public IActionResult HangingBag()
        {
            return View();
        }

        [Route("Smallroll")]
        public IActionResult Smallroll()
        {
            return View();
        }

        [Route("PPhot")]
        public IActionResult PPhot()
        {
            return View();
        }

        [Route("PEflower")]
        public IActionResult PEflower()
        {
            return View();
        }

        [Route("PEcow")]
        public IActionResult PEcow()
        {
            return View();
        }

        [Route("Elseelse")]
        public IActionResult Elseelse()
        {
            return View();
        }

        [Route("rubberband")]
        public IActionResult rubberband()
        {
            return View();
        }

        [Route("trash")]
        public IActionResult trash()
        {
            return View();
        }

        [Route("contact_us")]
        public IActionResult contact_us()
        {
            return View();
        }

        /// <summary>
        /// 上傳攤位資料到資料庫
        /// </summary>
        /// <param name="product_name">產品名稱</param>
        /// <param name="price">價格</param>
        /// <param name="quantity">數量</param>
        /// <param name="booth_name">攤位名</param>
        /// <param name="booth_num">攤號</param>
        /// <param name="phone">電話號碼</param>
        /// <param name="demo">備註</param>
        /// 
        /// 
        /// <param name="booth_name">攤位名</param>
        /// <param name="booth_num">攤號</param>
        /// <param name="totalPrice">總金額</param>
        /// <param name="time">時間</param>
        /// <returns></returns>
        /// 

        [HttpPost]
        [Route("update")]
        public bool update(string product_name, string price, string quantity, string booth_name, string booth_num, string phone, string demo)
        {
            string fullname = "";
            bool success = accountService.update(product_name, price, quantity, booth_name, booth_num, phone, demo, out fullname);
            if (success)
            {
                //ViewBag.msg = "下單成功。";
                //View("Index");
            }
            else
            {
                //ViewBag.msg = "下單失敗，請檢查是否輸入正確。 ";
            }
            return success;
        }


        [HttpPost]
        [Route("orderBag")]
        public bool orderBag(string booth_name, string booth_num, string total_price)
        {
            string fullname = "";
            bool success = accountService.orderBag( booth_name, booth_num, total_price, out fullname);
            if (success)
            {
                //ViewBag.msg = "下單成功。";
                //View("Index");
            }
            else
            {
                //ViewBag.msg = "下單失敗，請檢查是否輸入正確。 ";
            }
            return success;
        }

        [HttpPost]
        [Route("calcuate")]
        public JsonResult calcuate()
        {
            string fullname = "";
            List<List<object>> data = null;
            accountService.getInfo(out data);

            return Json(data);
        }

        [HttpPost]
        [Route("calcuate_all")]
        public JsonResult calcuate_all()
        {
            string fullname = "";
            List<List<object>> data = null;
            accountService.getInfo_all(out data);

            return Json(data);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            string fullname = "";
            bool success = accountService.Login(username, password, out fullname);
            if (success)
            {
                //HttpContext.Session.SetString("username", fullname);

                //return RedirectToAction("Welcome");
                return RedirectToAction("Check");
            }
            else
            {
                ViewBag.msg = "登入失敗";
                return View("Login_bao");
            }
        }

    }
}