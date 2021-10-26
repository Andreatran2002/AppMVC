## Controller
- Là một lớp kế thừa lớp Controller : Microsoft.AspNetCore.Mvc.Controller
- Action tỏng controller là một phuwogn thức public (không được static)
- Action trả về bất kỳ kiểu dữ liệu nào , thường là IActionResult
- Các dịch vụ inject vào controller qua hàm tạo

## View 
- Là file .cshtml
- View cho Action lưu tại /View/ControllerName/ActionName.cshtml
- Thêm thư mục lưu trữ View : 
```
// {0} -> Tên action
// {1} -> Tên controller
// {2} -> Tên area
options.ViewLocationFormats.Add("/MyViews/{1}/{0}"+RazorViewEngine.ViewExtension);

## Truyền dữ liệu sang view
-Model
-ViewData
-ViewBag
-TempData
