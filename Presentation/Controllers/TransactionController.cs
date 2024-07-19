using DTOs.Request.Transaction;
using DTOs.Response.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Extentions.Paginate;
using Services.Extentions;
using Services.Interface;

namespace Presentation.Controllers
{
	[Route("api/transaction")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService _service;

		public TransactionController(ITransactionService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get list transaction (optional: by condition)
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<PaginatedList<TransactionResponse>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery] GetListTransactionRequest request)
		{
			var transactions = await _service.GetList(request);
			if (transactions.TotalCount <= 0)
			{
				return StatusCode(404, new PetHealthCareResponse<PaginatedList<TransactionResponse>>(false, "Transactions not found", null));
			}
			return Ok(new PetHealthCareResponse<PaginatedList<TransactionResponse>>(true, "Transactions retrieved successfully", transactions));
		}

		/// <summary>
		/// Get transaction by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<TransactionResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var transaction = await _service.GetById(id);
			if (transaction == null)
			{
				return NotFound(new PetHealthCareResponse<TransactionResponse>(false, "Transaction not found", null));
			}
			return Ok(new PetHealthCareResponse<TransactionResponse>(true, "Transaction retrieved successfully", transaction));
		}
	}
}
