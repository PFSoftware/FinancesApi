using AutoMapper;
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
            CreateMap<FinancialTransaction, FinancialTransactionViewModel>().ReverseMap();
            CreateMap<MajorCategory, MajorCategoryViewModel>().ReverseMap();
            CreateMap<MinorCategory, MinorCategoryViewModel>().ReverseMap();
            CreateMap<Payee, PayeeViewModel>().ReverseMap();
        }
    }
}