using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odata.Service;

namespace Odata.Controllers
{
	[Route("odata/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService _service;

		public TransactionController(ITransactionService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetListTransaction()
		{
			var transactionList = await _service.GetList();

			if (transactionList == null || transactionList.Count == 0)
			{
				return NotFound();
			}
			return Ok(transactionList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var transaction = await _service.GetById(id);
			if (transaction == null)
			{
				return NotFound();
			}
			return Ok(transaction);
		}
	}
}
