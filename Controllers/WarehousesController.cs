using Microsoft.AspNetCore.Mvc;

namespace Controllers;

using Repositories;

[Route("api/[controller]")]
[ApiController]
public class WarehousesController : ControllerBase {
    private readonly IConfiguration _conf;
    private IWarehouseRepository _repo; 

    public WarehousesController(IConfiguration conf, IWarehouseRepository repo) {
        _conf = conf;
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> AddWarehouseProduct([FromBody] int IdProdukt, [FromBody] int IdWarehouse, [FromBody] int Amount, [FromBody] DateTime CreatedAt) {
        if(Amount <= 0) {
            return BadRequest();
        }
        if(!await _repo.CheckProductExists(IdProdukt)) {
            return NotFound();
        }
        if(!await _repo.CheckWarehouseExists(IdWarehouse)) {
            return NotFound();
        }
        int? IdOrder;
        if(!((IdOrder = await _repo.GetOrder(IdProdukt, Amount, CreatedAt)).HasValue)) {
            return NotFound();
        }
        if(await _repo.OrderRealized(IdProdukt, IdWarehouse, IdOrder.Value)) {
            return BadRequest();
        }
        return Ok();
    }

}
