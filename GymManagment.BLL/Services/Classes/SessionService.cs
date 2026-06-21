using AutoMapper;
using GymManagmemnt.BLL.ViewModels.SessionViewModel;
using GymManagment.BLL.Common;
using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels.SessionViewModel;
using GymManagment.DAL.Models;
using GymManagment.DAL.repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Classes
{
   public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SessionService(IUnitOfWork unitOfWork ,IMapper Mapper)
        {
          _unitOfWork = unitOfWork; 
         _mapper = Mapper;
        }

        public async Task<Result> CreateSessionAsync(CreateSessionViewModel model, CancellationToken ct = default)
        {
            if (model.EndDate <= model.StartDate) return Result.Validation("End Date Must Be Greater Than Start Date");
            if (model.StartDate <= DateTime.Now) return Result.Validation("Start Date Must be in the Future");
            if (model.Capacity < 1 || model.Capacity > 25) return Result.Validation("Capacity Must Be Between 1 and 25");
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(model.TrainerId);
            if (trainer == null) return Result.NotFound("Trainer Not Found");

            var category = await _unitOfWork.GetRepository<Catgory>().GetByIdAsync(model.CategoryId);
            if (category == null) return Result.NotFound("Category Not Found");

            var isValid = Enum.TryParse<Spacialty>(category.CatgoryName, true, out var CategorySpeciallty);
            if (!isValid || trainer.Spacialty != CategorySpeciallty) return Result.Validation("Trainer and Category Must be the Same !");

            var session = _mapper.Map<Session>(model);
            _unitOfWork.GetRepository<Session>().AddAsync(session);
           var result = await _unitOfWork.SaveChangesAsync();
            return result > 0? Result.OK() : Result.Fail("Failed to Create Session");
        }

        public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct = default) { 
                   var sessions = await _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(ct);
            if (sessions == null || !sessions.Any()) return null;
            var mappedSession = sessions.Select( S => new SessionViewModel() { 
            
            Id =S.Id,
                capacity = S.Capacity,
                CategoryName = S.Catgory.CatgoryName,
                TrainerName =S.Trainer.Name,
                StartDate = S.SatartDate,
                EndDate = S.EndDate,
                Description=S.Descriptio
            
            });
            foreach (var session in mappedSession)
            {
                session.AvailableSlots = session.Capacity - await _unitOfWork.SessionRepository.CountOfBookedSlotsAsync(session.Id, ct);




            }
   return mappedSession;
        }

        public async Task<IEnumerable<CategorySelectViewModel>> GetCategoryForDropDown(CancellationToken ct = default)
        {
          var result = await _unitOfWork.GetRepository<Catgory>().GetAllAsync(ct:ct);
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(result);
        }

        public async Task<Result<SessionViewModel>> GetSessionByIdAsync(int sessionId, CancellationToken ct = default)
        {
           var session = await _unitOfWork.SessionRepository.GetSessionByIdWithTrainerAndCatgory(sessionId, ct);
            if (session is null) return Result<SessionViewModel>.NotFound("session Not Found");

            var mappedSession =_mapper.Map<Session,SessionViewModel>(session);
            mappedSession.AvailableSlots = mappedSession.Capacity - await _unitOfWork.SessionRepository.CountOfBookedSlotsAsync(sessionId, ct);
            return Result<SessionViewModel>.OK(mappedSession);
        }

        public async Task<Result<UpdateSessionViewModel>> GetSessionToUpdate(int sessionId, CancellationToken ct = default)
        {
            var session = await _unitOfWork.SessionRepository.GetByIdAsync(  sessionId, ct);
            if (session is null) return Result<UpdateSessionViewModel>.NotFound("SessionNot Found");
            if (session.StartDate <= DateTime.Now)
                return Result<UpdateSessionViewModel>.Fail("cannot Update Ongoing Sessions !");
            var bookingCount = await _unitOfWork.SessionRepository.CountOfBookedSlotsAsync(sessionId,ct);

            if (bookingCount > 0)
                return Result<UpdateSessionViewModel>.Fail("Cannot Update Session Already Booked");

            var mappedSession = _mapper.Map<Session,UpdateSessionViewModel>( session);
            return Result<UpdateSessionViewModel>.OK(mappedSession);
        }

        public async Task<IEnumerable<TrainerSelectViewModel>> GetTrainerForDropDown(CancellationToken ct = default)
        {
            var result = await _unitOfWork.GetRepository<Trainer>().GetAllAsync(ct: ct);
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(result);
        }

        public Task<Result> UpdateSessionAsync(int id, UpdateSessionViewModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        Task<bool> ISessionService.CreateSessionAsync(CreateSessionViewModel model, CancellationToken ct)
        {
            
        }
    }
}
