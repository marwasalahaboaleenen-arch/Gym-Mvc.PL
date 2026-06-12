
namespace Gym_Mvc.PL.Controllers
{
    public interface IPlanRepository
    {
        Task<string?> GetAllAsync(CancellationToken ct);
        Task<string?> GetByIdAsync(int id, CancellationToken ct);
    }
}