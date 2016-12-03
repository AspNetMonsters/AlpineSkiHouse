using AlpineSkiHouse.Data;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Web.Models.PassViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Controllers
{
    public class PassController : Controller
    {
        PassContext _passContext;
        PassTypeContext _passTypeContext;

        public PassController(PassContext passContext, PassTypeContext passTypeContext)
        {
            _passTypeContext = passTypeContext;
            _passContext = passContext;
        }
        [HttpGet]
        public ActionResult Create(int skiCardId)
        {
            return View(new Pass { CardId = skiCardId });
        }
        [HttpPost]
        public async Task<ActionResult> Create(Pass pass)
        {
            pass.CreatedOn = DateTime.UtcNow;
            _passContext.Add(pass);
            await _passContext.SaveChangesAsync();
            return RedirectToAction("Index", new { skiCardId = pass.CardId });
        }
        public async Task<ActionResult> Index(int skiCardId)
        {
            var passTypes = await _passTypeContext.PassTypes.ToListAsync();
            var passes = new List<ListViewModelItem>();
            foreach (var pass in (await _passContext.Passes.Where(x => x.CardId == skiCardId).ToListAsync()))
            {
                var passType = passTypes.First(x => x.Id == pass.PassTypeId);
                passes.Add(new ListViewModelItem
                {
                    Name = passType.Name,
                    Description = passType.Description,
                    ValidFrom = passType.ValidFrom,
                    ValidTo = passType.ValidTo
                });
            }
            var model = new ListViewModel
            {
                SkiCardId = skiCardId,
                Passes = passes
            };
            return View(model);
        }
    }
}
