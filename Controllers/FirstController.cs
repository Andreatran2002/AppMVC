
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Net.Models;
using System.IO;
using App.Services;

namespace App.Net.Controllers
{
    public class FirstController : Controller
    {
        // Controller chứa các thông tin 
        //this.HttpContext
        //this.Request
        //this.Response
        //this.RouteData

        //this.User
        //this.ModelState
        //this.ViewData
        //this.ViewBag
        //this.Url
        //this.TempData
        private readonly ProductService _productService;
        private readonly ILogger<FirstController> _logger;

        public FirstController(ILogger<FirstController> logger,
                               ProductService productService)
        {
            _logger = logger;
            _productService = productService; 
        }

        [TempData]
        public string StatusMessage{set;get;}
        public string Test(){
            _logger.LogInformation("Index Action"); 

            _logger.Log(LogLevel.Warning,"Thông báo abc");
            _logger.LogWarning("Thông báo abc");
            //SeriLog có thể cấu hình log sử dụng thông qua ILogger với kĩ thuật DI 
            return "Đây là nội dung test"; 
        } 
            
        public void Nothing(){
            _logger.LogInformation("Do nothing");
            Response.Headers.Add("Hi","Vy");
        }
    //      ContentResult               | Content()
    // EmptyResult                 | new EmptyResult()
    // FileResult                  | File()
    // ForbidResult                | Forbid()
    // JsonResult                  | Json()
    // LocalRedirectResult         | LocalRedirect()
    // RedirectResult              | Redirect()
    // RedirectToActionResult      | RedirectToAction()
    // RedirectToPageResult        | RedirectToRoute()
    // RedirectToRouteResult       | RedirectToPage()
    // PartialViewResult           | PartialView()
    // ViewComponentResult         | ViewComponent()
    // StatusCodeResult            | StatusCode()
    // ViewResult                  | View()
        
        public IActionResult Readme(){
            var content = @"hêl
                            lo";
            return Content(content,"text/plain");
        }

        public IActionResult Image(){
            // Startup.ContentRootPath;
            string filePath  =Path.Combine(Startup.ContentRootPath,"Files","cat.jpg");  
            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes,"image/jpg");
        }

        public IActionResult JsonResult(){
            return Json(
                new{
                    product ="iphone",
                    price = 3345
                }
            ); 
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HelloView(string username)
        {
            //View() => Razor engine , doc cshtml
            if (string.IsNullOrEmpty(username)){
                username = "khách"; 
                
            }
            //View() => Razor engine , doc .cshtml
            //View(template) - template đường dẫn tuyệt đối tới cshtml
            //View(template,model)
            // return View("/MyViews/index.cshtml",username);

            //hello2.cshtml -> /View/First/hello2.cshtml
            // return View("hello2",username);

            //HelloView.cshtml -> View/First/helloview.cshtml
            // return View(); 
            // return View((object)username); 

            return View("hello3",username);
            // PHổ biến :
            // View() ; và View(Model);
        }

        public IActionResult ViewProduct(int ?id)
        {
            var product = _productService.Where(p =>p.Id == id).FirstOrDefault();
            if (product == null) {
                // TempData["StatusMessage"] = "San pham khong ton tai"; 
                StatusMessage ="Sản phẩm bạn yêu cầu không có";
                return Redirect(Url.Action("Index","Home"));  
            }
            //MyViews/First/ViewProduct.cshtml
            //Model
            // return View(product);
        
            //ViewData
            // this.ViewData["product"] = product; 
            // ViewData["Title"] = product.Name;            
            // return View("ViewProduct2"); 


            //ViewBag
             ViewBag.product = product; 
            // return View("ViewProduct3");
            // Khi sử dụng tempdata thì khi dữ liệu được đọc từ trang khác sẽ xóa 
            // luôn
            // TempData["Thongbao"] = "abc";
            
            return View("ViewProduct3"); 


        }

        public IActionResult Privacy()
        {
            var url = Url.Action("Privacy","Home"); 
            _logger.LogInformation("Chuyển hướng đến " + url); 
            return LocalRedirect(url);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
