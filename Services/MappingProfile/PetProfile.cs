using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request.Pet;
using DTOs.Response.Pet;

namespace Services.MappingProfile
{
    public class PetProfile : Profile
	{
        public PetProfile()
        {
			CreateMap<Pet, PetResponse>();
			CreateMap<CreatePetRequest, Pet>();
			CreateMap<UpdatePetRequest, Doctor>();
		}
    }
}
