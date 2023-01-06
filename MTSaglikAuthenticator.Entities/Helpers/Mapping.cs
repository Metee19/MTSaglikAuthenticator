using AutoMapper;
using MTSaglikAuthenticator.Entities.Models;
using MTSaglikAuthenticator.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Institution, InstitutionViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Title, TitleViewModel>().ReverseMap();
            CreateMap<File, FileViewModel>().ReverseMap();
        }
    }
}
