using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_MyProject.DAL;
using Rocket_MyProject.Models;
using Rocket_MyProject.ViewModels.Portfolios;
using System.Diagnostics;

namespace Rocket_MyProject.Controllers
{
    public class HomeController(PortfolioContext _sql) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _sql.Portfolies
                .Select(a=> new GetPortfolioVM
                {
                    Id = a.Id,
                    Image = a.Image,
                }).ToListAsync());
        }


    }
}
