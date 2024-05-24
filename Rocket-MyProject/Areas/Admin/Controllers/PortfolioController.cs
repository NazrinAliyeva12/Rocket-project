using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_MyProject.DAL;
using Rocket_MyProject.Extentions;
using Rocket_MyProject.Models;
using Rocket_MyProject.ViewModels.Portfolios;

namespace Rocket_MyProject.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize]

public class PortfolioController(PortfolioContext _sql, IWebHostEnvironment _env) : Controller
{
	public async Task<IActionResult> Index()
	{
		return View(await _sql.Portfolies
			.Select(a => new GetPortfolioAdminVM
			{
				Id = a.Id,
				Image = a.Image,
			}).ToListAsync());
	}
	public IActionResult Create()
	{
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> Create(CreatePortfolioAdminVM data)
	{

		if (data.Photo != null)
		{
			if (!data.Photo.IsValidType("image"))
			{
				ModelState.AddModelError("Photo", "Faylin novu uygun deyil.");
				return View();
			}
			if (!data.Photo.IsValidLength(900))
			{
				ModelState.AddModelError("Photo", "Fayin hecmi 900-den cox ola bilmez.");
				return View();
			}
		}
		if (!ModelState.IsValid)
		{
			return View();
		}

		string fileName = await data.Photo.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "prts"));


		Portfolio port = new Portfolio()
		{
			Image = fileName
		};

		await _sql.Portfolies.AddAsync(port);
		await _sql.SaveChangesAsync();
		return RedirectToAction(nameof(Index));

	}


    public async Task<IActionResult> Update(int? id)
	{
        if (id == null || id < 1) return BadRequest();
        Portfolio portfolio = await _sql.Portfolies.FirstOrDefaultAsync(p => p.Id == id);
        if (portfolio == null) return NotFound();
        UpdateVM updateVM = new UpdateVM
        {
            Image = portfolio.Image
        };
        return View(updateVM);
    }
	[HttpPost]
	public async Task<IActionResult>Update(int? id, UpdateVM updateVM)
	{

        if (id == null || id < 1) return BadRequest();
        Portfolio existed = await _sql.Portfolies.FirstOrDefaultAsync(p => p.Id == id);
        if (existed == null) return NotFound();

        existed.Image = updateVM.Image;
		await _sql.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult>Delete(int? id)
	{
        if (id == null) return BadRequest();
        var cat = await _sql.Portfolies.FindAsync(id);
        if (cat is null) return NotFound();
        System.IO.File.Delete(Path.Combine(_env.WebRootPath, cat.Image));
        _sql.Remove(cat);
        await _sql.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}































