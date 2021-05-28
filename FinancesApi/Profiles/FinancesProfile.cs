using AutoMapper;
using PFSoftware.FinancesApi.Models.Api.Requests;
using PFSoftware.FinancesApi.Models.Domain;
using PFSoftware.FinancesApi.Models.ViewModels;

namespace PFSoftware.FinancesApi.Profiles
{
    public class FinancesProfile : Profile
    {
        public FinancesProfile()
        {
            //Source -> Target
            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Account, CreateEditAccountRequest>().ReverseMap();
            CreateMap<FinancialTransaction, FinancialTransactionViewModel>().ReverseMap();
            CreateMap<FinancialTransaction, CreateEditFinancialTransactionRequest>().ReverseMap();
            CreateMap<MajorCategory, MajorCategoryViewModel>().ReverseMap();
            CreateMap<MajorCategory, CreateEditMajorCategoryRequest>().ReverseMap();
            CreateMap<MinorCategory, MinorCategoryViewModel>().ReverseMap();
            CreateMap<MinorCategory, CreateEditMinorCategoryRequest>().ReverseMap();
            CreateMap<Payee, PayeeViewModel>().ReverseMap();
            CreateMap<Payee, CreateEditPayeeRequest>().ReverseMap();
        }
    }
}