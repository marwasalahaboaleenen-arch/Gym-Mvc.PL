
using GymSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMmangment.DAL.repositries.Interfaces
{
    public interface IplanRepositiry
    {
        //GETAllPlans
         Task<IEnumerable<Plan>> GetAllAsync(bool tracking = false, CancellationToken ct = default);

        //GETPlansById
        Task<Plan?> GETPlansById(int id, CancellationToken ct = default);


        //Add
        Task<int> AddAsync(Plan plan, CancellationToken ct = default);

        //Update
        Task<int> UpdateAsync(Plan plan, CancellationToken ct = default);

        //Delete
        Task<int> DeleteAsync(Plan plan, CancellationToken ct = default);
    }
}
