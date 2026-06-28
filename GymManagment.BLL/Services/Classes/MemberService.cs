using AutoMapper;
using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.DAL.Models;
using GymManagment.DAL.repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private bool ID;
        private IEnumerable<object> members;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public object ActiveMembership { get; private set; }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            //Email Exist or Not
            var EmailExist = await _unitOfWork.GetRepository<member>().AnyAsync(X => X.Email == model.Email);
            //phone Exist or Not
            var PhoneExist = await _unitOfWork.GetRepository<member>().AnyAsync(X => X.Phone == model.Phone);
            if (EmailExist || PhoneExist) return false;

            var member = _mapper.Map<member>(model);
             _unitOfWork.GetRepository<member>().AddAsync(member);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;



        }

        public async Task<bool> DeleteMemberAsync(int memberId, CancellationToken ct = default)
        {
            var member = await _unitOfWork.GetRepository<member>().GetByIdAsync(memberId,ct);

            if (member == null) return false;

            var HasActiveBooking = await _unitOfWork.GetRepository<Booking>().AnyAsync(B => B.MemberId == memberId && B.Id = ID);
                if(HasActiveBooking) return false;
                _unitOfWork.GetRepository<member>().DeleteAsync(member);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllAsync(CancellationToken ct)
        {
           _unitOfWork.GetRepository<member>().GetAllAsync(ct: ct);

            if (!members.Any())
                return Enumerable.Empty<MemberViewModel>();

            List<MemberViewModel> memberVM = new();

            foreach (var member in members)
            {
                //Manual Mapping
                var memberViewModel = new MemberViewModel
                {
                    Id = member.Id,
                    Name = member.Name,
                    Phone = member.Phone,
                    Email = member.Email,
                    Photo = member.photo,
                    Gender = member.Gender.ToString(),
                };
                memberVM.Add(memberViewModel);
            }
            return memberVM;
        }

        public async Task<MemberViewModel?> GetMemberDetailsByIdAsync(int memberId, CancellationToken ct = default)
        {
            var member = await _unitOfWork.GetRepository<member>().GetByIdAsync(memberId, ct);


            if (member == null) return null;

            //Table = Member
            //Return = MemberViewModel

            var model = _mapper.Map< member,MemberViewModel>(member);

            //Check if Member Has ActiveMembership or Not
            if (ActiveMembership is not null)

            {
                var ActiveMembership = await _unitOfWork.GetRepository<Membership>().FristOrDefaultAsync(X => X.MemberId == memberId && X.EndDate > DateTime.Now);

                var ActivePlan = await _unitOfWork.GetRepository<Membership>().GetByIdAsync(ActiveMembership.PlanId, ct);
                model.Name = ActivePlan?.Name;
                model.MembershipStartDate = ActiveMembership.CreatedAt.ToString();
                model.MembershipEndDate = ActiveMembership.EndDate.ToString();
            }
            return model;
        }

        public async Task<HealthRecordViewModel?> GetMemberHealthRecord(int memberId, CancellationToken ct = default)
        {
            var recored = await _unitOfWork.GetRepository<member>().FristOrDefaultAsync(X => X.MemberId == memberId, ct: ct);
            if (recored is null) return null;
            else
                return _mapper.Map<HealthRecord, HealthRecordViewModel>(recored);
        }

        public async Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int memberId, CancellationToken ct = default)
        {
           var member = await _unitOfWork.GetRepository<member>().GetByIdAsync(memberId, ct);
            if (member is null) return null;
            else
                return _mapper.Map<MemberToUpdateViewModel>(member);
        }

        public async Task<bool> UpdateMemberAsync(int id, MemberToUpdateViewModel model, CancellationToken ct)
        {

            //Get Member
            var member = await _unitOfWork.GetRepository<member>().GetByIdAsync(id, ct);

            //check if Any other User Has The Same Phone or Email


                var EmailExist = await _unitOfWork.GetRepository<member>().AnyAsync(M => M.Email == model.Email && M.Id !=id);
            var PhoneExist = await _unitOfWork.GetRepository<member>().AnyAsync(M = M => M.Phone == model.Phone && M.Id !=id);

            if (EmailExist || PhoneExist) return false;
            {   _mapper.Map<member>(model);
                member.UpadatedAt = DateTime.Now;

                _unitOfWork.GetRepository<member>().UpdateAsync(member);
                var result = await _unitOfWork.SaveChangesAsync(ct);    
                return result > 0;


            }

        }
    }
}