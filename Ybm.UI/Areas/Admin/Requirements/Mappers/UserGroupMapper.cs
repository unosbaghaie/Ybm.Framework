using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ybm.Common.Models;
using Ybm.UI.Areas.Admin.Requirements.ViewModels;

namespace Ybm.UI.Areas.Admin.Requirements
{
    public static class UserGroupMapper
    {


        private static IMapper GetViewModelMapper { get; set; }
        private static IMapper GetDomainModelMapper { get; set; }
        public static UserGroupViewModel GetViewModel(this UserGroup domain)
        {
            if (GetViewModelMapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //cfg.CreateMap<string, int>().ConvertUsing<TypeTypeConverter>();
                    cfg.CreateMap<UserGroup, UserGroupViewModel>()
                    //  .ForMember(q=>q.UserGroup_Id, src=>src.MapFrom(z=>z.))
                    ;
                    //.ForMember(q => q.Description, o => o.Ignore())
                });
                GetViewModelMapper = config.CreateMapper();
            }
            var dest = GetViewModelMapper.Map<UserGroup, UserGroupViewModel>(domain);
            return dest;
        }
        public static UserGroup GetDomainModel(this UserGroupViewModel viewModel)
        {
            if (GetViewModelMapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserGroupViewModel, UserGroup>();
                });

                GetDomainModelMapper = config.CreateMapper();
            }
            var dest = GetDomainModelMapper.Map<UserGroupViewModel, UserGroup>(viewModel);
            return dest;
        }
    }
    //public class TypeTypeConverter : ITypeConverter<string, int>
    //{
    //    public int Convert(string source, int destination, ResolutionContext context)
    //    {
    //        return System.Convert.ToInt32(source);
    //    }
    //}
}