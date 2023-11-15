using Fundipedia.TechnicalInterview.Data.Context;
using Fundipedia.TechnicalInterview.Domain.ServiceResponse;
using Fundipedia.TechnicalInterview.Model.Extensions;
using Fundipedia.TechnicalInterview.Model.Supplier;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundipedia.TechnicalInterview.Domain;

public class SupplierService : ISupplierService
{
    private readonly SupplierContext _context;

    public SupplierService(SupplierContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Supplier>>> GetSuppliers()
    {
        var serviceResponse = new ServiceResponse<List<Supplier>>();

        try
        {
            serviceResponse.Data = await _context.Suppliers
                .Include(s => s.Emails)
                .Include(s => s.Phones)
                .ToListAsync();

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"An error occured: {ex.Message}";

            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Supplier>> GetSupplier(Guid id)
    {
        var serviceResponse = new ServiceResponse<Supplier>();

        try
        {
            var supplier = await _context.Suppliers
                .Include(s => s.Emails)
                .Include(s => s.Phones)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Supplier with id: {id} - not found";

                return serviceResponse;
            }

            serviceResponse.Success = true;
            serviceResponse.Data = supplier;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"An error occured: {ex.Message}";

            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Supplier>> InsertSupplier(Supplier supplier)
    {
        var serviceResponse = new ServiceResponse<Supplier>
        {
            Success = false
        };

        if (!(supplier.ActivationDate >= DateTime.Today.AddDays(1)))
        {
            serviceResponse.Message = $"Invalid Activation date: {supplier.ActivationDate}";

            return serviceResponse;
        }
        if (supplier.IsValidEmail(out var invalidEmail) is false)
        {
            serviceResponse.Message = $"Invalid email address: {invalidEmail}";

            return serviceResponse;
        }
        if (supplier.IsValidPhoneNumber(out var invalidPhoneNumber) is false)
        {
            serviceResponse.Message = $"Invalid phone number: {invalidPhoneNumber}";

            return serviceResponse;
        }

        try
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            serviceResponse.Data = supplier;
            serviceResponse.Success = true;
            serviceResponse.Message = "Supplier added successfully";

            return serviceResponse;

        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"An error occured: {ex.Message}";

            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<bool>> DeleteSupplier(Guid id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        var supplier = await _context.Suppliers.FindAsync(id);

        if (supplier is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Supplier with id: {id} - not found";

            return serviceResponse;
        }

        if (supplier.IsActive())
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Supplier {id} is active, can't be deleted";

            return serviceResponse;
        }

        try
        {
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            serviceResponse.Success = true;
            serviceResponse.Message = $"Supplier with id: {id} - deleted";

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"An error occured: {ex.Message}";

            return serviceResponse;
        }
    }
}
