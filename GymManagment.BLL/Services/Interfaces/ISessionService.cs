using GymManagmemnt.BLL.ViewModels.SessionViewModel;
using GymManagment.BLL.Common;
using GymManagment.BLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Interfaces
{
   public interface ISessionService
    {

        Task<IEnumerable<SessionViewModel>>GetAllSessionsAsync(CancellationToken ct=default);
        Task<Result>CreateSessionAsync(CreateSessionViewModel model, CancellationToken ct=default);
        Task<IEnumerable<TrainerSelectViewModel>>GetTrainerForDropDown(CancellationToken ct=default);
        Task<IEnumerable<CategorySelectViewModel>> GetCategoryForDropDown(CancellationToken ct = default);
        Task<Result<SessionViewModel>> GetSessionByIdAsync(int sessionId,CancellationToken ct=default);

        Task<Result<UpdateSessionViewModel>> GetSessionToUpdate(int sessionId,CancellationToken ct=default);
        Task<Result>UpdateSessionAsync(int id , UpdateSessionViewModel model, CancellationToken ct=default);
        
    }
}
