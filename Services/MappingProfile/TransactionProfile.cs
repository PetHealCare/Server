using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request.Staff;
using DTOs.Request.User;
using DTOs.Response.Staff;
using DTOs.Response.Transaction;

namespace Services.MappingProfile
{
	public class TransactionProfile : Profile
	{
		public TransactionProfile()
		{
			CreateMap<Transaction, TransactionResponse>();
		}
	}
}
