using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Edit()
    {
        return RedirectToAction("Index");
    }
}