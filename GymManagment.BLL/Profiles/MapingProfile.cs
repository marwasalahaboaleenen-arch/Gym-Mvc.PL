using AutoMapper;
using GymManagmemnt.BLL.ViewModels.SessionViewModel;
using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.BLL.ViewModels.SessionViewModel;
using GymManagment.DAL.Models;
using Microsoft.Data.SqlClient;
using System.IO.Compression;

namespace GymManagment.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MemberProfiles();
            SessionProfiles();
                   

        }
        private void MemberProfiles()
        {
            CreateMap<member, MemberViewModel>()
                  .ForMember(
                      dest => dest.Address,
                      opt => opt.MapFrom(src =>
                          $"{src.Address.BuildingNumb} - {src.Address.Street} - {src.Address.City}"
                      )
                  )
                  .ForMember(
                      dest => dest.DateOfBirth,
                      opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString())
                  );

            CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap();

            CreateMap<member, MemberToUpdateViewModel>()
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => src.Address.City)
                )
                .ForMember(
                    dest => dest.Street,
                    opt => opt.MapFrom(src => src.Address.Street)
                )
                .ForMember(
                    dest => dest.BuildingNumber,
                    opt => opt.MapFrom(src => src.Address.BuildingNumb)
                );

            CreateMap<MemberToUpdateViewModel, member>()
                .ForMember(dest => dest.photo, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address ??= new Address();

                    dest.Address.BuildingNumb = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                });
            CreateMap<MemberToUpdateViewModel, member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
                {
                    BuildingNumb = src.BuildingNumber,
                    City = src.City,
                    Street = src.Street,
                }))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));
        }
        private void SessionProfiles()
        {
            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Catgory, CategorySelectViewModel>();
            CreateMap<Trainer, TrainerSelectViewModel>();

            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(Src => Src.Trainer.Name))
                .ForMember(dest => dest.CatgoryName, opt => opt.MapFrom(Src => Src.Catgory.CatgoryName));
            CreateMap<Session, UpdateSessionViewModel>();


        }

        private void CreateMap<T1, T2>()
        {
            throw new NotImplementedException();
        }
    }
}