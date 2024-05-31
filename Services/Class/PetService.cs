﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request;
using DTOs.Response;
using Repositories.Interface;
using Services.Interface;

namespace Services.Class
{
	public class PetService : IPetService
	{
		private readonly IPetRepository _repo;
		private readonly IMapper _mapper;
		public PetService(IPetRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<PetResponse> Create(CreatePetRequest request)
		{
			var pet = new Pet()
			{
				Name = request.Name,
				Species = request.Species,
				Status = true,
				CustomerId = request.CustomerId,
			};

			var petCreated = await _repo.Create(pet);
			var response = _mapper.Map<PetResponse>(pet);
			return response;
		}

		public async Task<PetResponse> GetById(int id)
		{
			var pet = await _repo.GetPetById(id);
			var response = _mapper.Map<PetResponse>(pet);
			return response;
		}

		public async Task<List<PetResponse>> GetList(GetListPetRequest request)
		{
			var listPetQuery = (await _repo.GetList()).AsQueryable();
			if (!string.IsNullOrEmpty(request.Name))
			{
				listPetQuery = listPetQuery.Where(p => p.Name.Contains(request.Name));
			}

			if (!string.IsNullOrEmpty(request.Species))
			{
				listPetQuery = listPetQuery.Where(p => p.Species.Contains(request.Species));
			}

			if(request.CustomerId != null)
			{
				listPetQuery = listPetQuery.Where(p => p.CustomerId == request.CustomerId);
			}
			var listPet = listPetQuery.ToList();
			var response = _mapper.Map<List<PetResponse>>(listPet);
			return response;
		}

		public async Task<PetResponse> Update(UpdatePetRequest request)
		{
			var pet = await _repo.GetPetById(request.PetId);
			if (pet == null)
			{
				return null; // or throw an exception, based on your error handling strategy
			}

			// Update the fields
			pet.Name = request.Name ?? pet.Name;
			pet.Species = request.Species ?? pet.Species;
			pet.CustomerId = request.CustomerId;

			// Save changes
			await _repo.Update(pet);

			// Map updated pet to response
			var response = _mapper.Map<PetResponse>(pet);
			return response;
		}

	}
}
