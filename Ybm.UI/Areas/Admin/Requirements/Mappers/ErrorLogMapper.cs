using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ybm.Common.Models;
using Ybm.UI.Areas.Admin.Requirements.ViewModels;

namespace Ybm.UI.Areas.Admin.Requirements
{
    public static class ErrorLogMapper
    {


        private static IMapper GetViewModelMapper { get; set; }
        private static IMapper GetDomainModelMapper { get; set; }
        public static ErrorLogViewModel GetViewModel(this ErrorLog domain)
        {
            if (GetViewModelMapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //cfg.CreateMap<string, int>().ConvertUsing<TypeTypeConverter>();
                    cfg.CreateMap<ErrorLog, ErrorLogViewModel>()
                    //  .ForMember(q=>q.ErrorLog_Id, src=>src.MapFrom(z=>z.))
                    ;
                    //.ForMember(q => q.Description, o => o.Ignore())
                });
                GetViewModelMapper = config.CreateMapper();
            }
            var dest = GetViewModelMapper.Map<ErrorLog, ErrorLogViewModel>(domain);
            return dest;
        }
        public static ErrorLog GetDomainModel(this ErrorLogViewModel viewModel)
        {
            if (GetViewModelMapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ErrorLogViewModel, ErrorLog>();
                });

                GetDomainModelMapper = config.CreateMapper();
            }
            var dest = GetDomainModelMapper.Map<ErrorLogViewModel, ErrorLog>(viewModel);
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