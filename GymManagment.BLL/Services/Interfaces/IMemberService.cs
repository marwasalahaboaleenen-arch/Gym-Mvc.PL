using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Interfaces
{
    public interface IMemberService
    {
        //Get All
         Task<IEnumerable<MemberViewModel>> GetAllAsync(CancellationToken ct = default);

        Task<bool>CreateMemberAsync(CreateMemberViewModel member ,CancellationToken ct = default);

        //Get Member Details

        Task<MemberViewModel?> GetMemberDetailsByIdAsync(int memberId , CancellationToken ct = default);
        Task<HealthRecordViewModel?> GetMemberHealthRecord(int memberId , CancellationToken ct = default);
        Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int memberId , CancellationToken ct = default);
        Task<bool>UpdateMemberAsync(int id, MemberToUpdateViewModel model,CancellationToken ct);
        Task<bool> DeleteMemberAsync(int memberId, CancellationToken ct = default);

    }
}
