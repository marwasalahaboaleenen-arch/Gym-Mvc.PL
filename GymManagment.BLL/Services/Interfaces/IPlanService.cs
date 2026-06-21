using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.BLL.ViewModels.PlanViewModels;


namespace GymManagment.BLL.Services.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanViewModel?>> GetAllPlansAsync(CancellationToken ct = default);
        Task<PlanViewModel?> GetPlanDetailsByIdAsync(int planId, CancellationToken ct = default);
        Task<UpdatePlanViewModel?> GetPlanToUpdate(int planId, CancellationToken ct = default);
        Task<bool> UpdatePlanDetailsAsync(int id, UpdatePlanViewModel model, CancellationToken ct = default);
        Task<bool> TogglePlanActivationAsync(int planId, CancellationToken ct = default);
    }
}
