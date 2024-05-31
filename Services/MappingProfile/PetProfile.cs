using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DTOs.Response;

namespace Services.MappingProfile
{
	public class PetProfile : Profile
	{
        public PetProfile()
        {
			CreateMap<Pet, PetResponse>();
		}
    }
}
