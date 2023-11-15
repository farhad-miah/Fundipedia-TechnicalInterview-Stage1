using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fundipedia.TechnicalInterview.Model.Supplier;
using Fundipedia.TechnicalInterview.Domain;
using Fundipedia.TechnicalInterview.Domain.ServiceResponse;

namespace Fundipedia.TechnicalInterview.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    // GET: api/Suppliers
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Supplier>>>> GetSuppliers()
    {
        var response = await _supplierService.GetSuppliers();

        if (response.Success is false)
        {
            return response;
        }

        return Ok(response);
    }

    // GET: api/Suppliers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Supplier>>> GetSupplier(Guid id)
    {
        var response = await _supplierService.GetSupplier(id);

        if (response.Success is false)
        {
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return response;
        }

        return Ok(response);
    }

    // POST: api/Suppliers
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Supplier>>> PostSupplier(Supplier supplier)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var response = await _supplierService.InsertSupplier(supplier);

        if (response.Success is false)
        {
            return response;
        }

        return Ok(response);
    }

    // DELETE: api/Suppliers/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteSupplier(Guid id)
    {
        var response = await _supplierService.DeleteSupplier(id);

        if (response.Success is false)
        {
            return response;
        }

        return Ok(response);
    }
}