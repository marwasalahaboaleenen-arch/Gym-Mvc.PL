using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gym_Mvc.PL.Controllers
{
    public class Member_Controller : Controller
    {
        private readonly IMemberService _memberService;

        public Member_Controller (IMemberService memberService)
        {
            _memberService = memberService;
        }


        #region Get Members
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var members = await _memberService.GetAllAsync(ct);
            return View(members);
        }
        //GET : :BaseUrl/Members/Details/{id} => Get Specific Member
        public async Task<IActionResult> MemberDetails(int id, CancellationToken ct)
        {
            var member = await _memberService.GetMemberDetailsByIdAsync(id ,ct);
            if (member is null) {
                TempData["ErrorMessage"] = "Member Not Found !";
            }
            return View(member);
        }

        public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken ct)
        {


            var recored = await _memberService.GetMemberHealthRecord(id ,ct);
            if (recored is null)
            {
                TempData["ErrorMessage"] = "No Health Recored Found !";
                return RedirectToAction(nameof(Index));
            }
            return View(recored);
        }
        #endregion

        #region Create
        //Get
        [HttpGet]
        public IActionResult Create()
            => View();


        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model , CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(nameof(Create),model);

            var result =await _memberService.CreateMemberAsync(model, ct);

            if (result)
                TempData["SuccessMessage"] = "Member Createed Succesfully";
            else
                TempData["ErrorMessage"] = "Member Failed To Create !";


                return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Eidt

        //Get
        [HttpGet]
        public async Task<IActionResult>EditMember(int id,CancellationToken ct)
        {
          var member = await _memberService.GetMemberToUpdateAsync(id,ct);  
            if(member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found !";
                return RedirectToAction(nameof(Index));
            }

            return View(member);





        }

        [HttpPost]

        public async Task<IActionResult> EditMember(int id ,MemberToUpdateViewModel model , CancellationToken ct)
        {

            if (!ModelState.IsValid) return View(model);


            var result = await _memberService.UpdateMemberAsync(id,model, ct);
            if (result)
                TempData["SuccessMessage"] = "Member Update Successfully";
            else
                TempData["ErrorMessage"] = "Failed To Update Member";
            return RedirectToAction(nameof(Index));
        }





        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int id, CancellationToken ct) { 
        
        var member =await _memberService.GetMemberDetailsByIdAsync(id,ct);
            if(member == null)
            {

                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct) {
        var result = await _memberService.DeleteMemberAsync(id,ct);
            if (result)
                TempData["SuccessMessage"] = "Member Delete Successfully ";
            else
                TempData["ErrorMessage"] = "Failed to Delete Member";
            return RedirectToAction(nameof(Index));







        }










        #endregion
       
    }
}
