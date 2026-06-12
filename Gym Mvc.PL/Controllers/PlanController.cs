using Microsoft.AspNetCore.Mvc;

namespace Gym_Mvc.PL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository planRepository;

        public PlanController(IPlanRepository planRepository)
        {
            this.planRepository = planRepository;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await planRepository.GetAllAsync(ct: ct); // Pass By Name
            return View(plans);
        }
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var plan = await planRepository.GetByIdAsync(id, ct);

            if (plan == null)
                return RedirectToAction(nameof(Index));
            return View(plan);

        }
    }
}
