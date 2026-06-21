using GymManagmemnt.BLL.ViewModels.SessionViewModel;
using GymManagment.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace Gym_Mvc.PL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionServ;
        private string? model;

        public SessionController(ISessionService sessionService)
        {
          _sessionServ = sessionService;  
        }




        #region Get

        
        public async Task <IActionResult> Index(CancellationToken ct)
        {
            var sessions = await _sessionServ.GetAllSessionsAsync(ct);
            return View(sessions);
        }

        public async Task<IActionResult> Details(int id ,CancellationToken ct)
        {
          var result = await _sessionServ.GetSessionByIdAsync(id,ct);
          if(result.success)
                return View(result.value);
          else
            {
                TempData["ErrorMessage"] = result.error;
                result RedirectToAction(nameof(Index));
            }

        }


        #endregion
        #region Create
        [HttpGet]
        public async IActionResult Create() {
            DropDownList();
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel ,CancellationToken ct) {

            if (!ModelState.IsValid)
            {
                await DropDownList();
                return View(model);
            }
        
        var result=await _sessionServ.CreateSessionAsync(model,ct);
            if(result.Success)
            {
                TempData["SuccessMessage"] = "Session Created";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Failed To Create Session";
            DropDownList();
            return View(model);
        
        
        
        }
        private async Task DropDownList()
        {

            ViewBag.Trainers = new SelectList(await _sessionServ.GetTrainerForDropDown(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _sessionServ.GetCategoryForDropDown(), "Id", "CatgoryName");
        }
        #endregion
    }
}
