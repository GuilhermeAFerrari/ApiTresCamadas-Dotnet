using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevIO.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuppliersController : MainController
{
    private readonly IMapper _mapper;
    private readonly ISupplierRepository _supplierRepository;
    private readonly ISupplierService _supplierService;

    public SuppliersController(
        IMapper mapper,
        ISupplierRepository supplierRepository,
        ISupplierService supplierService,
        INotifier notifier) : base(notifier)
    {
        _mapper = mapper;
        _supplierRepository = supplierRepository;
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<IEnumerable<SupplierViewModel>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SupplierViewModel>> GetByIdAsync(Guid id)
    {
        var supplier = await GetSupplierProductsAddressAsync(id);

        if (supplier == null) return NotFound();

        return supplier;
    }

    [HttpPost]
    public async Task<ActionResult<SupplierViewModel>> AddAsync(SupplierViewModel supplierViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _supplierService.AddAsync(_mapper.Map<Supplier>(supplierViewModel));

        return CustomResponse(HttpStatusCode.Created, supplierViewModel);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SupplierViewModel>> UpdateAsync(Guid id, SupplierViewModel supplierViewModel)
    {
        if (id != supplierViewModel.Id)
        {
            NotificarErro("The id entered is not the same as the one passed in the query");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _supplierService.UpdateAsync(_mapper.Map<Supplier>(supplierViewModel));

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<SupplierViewModel>> RemoveAsync(Guid id)
    {
        await _supplierService.RemoveAsync(id);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<SupplierViewModel> GetSupplierProductsAddressAsync(Guid id)
    {
        return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierProductsAddressAsync(id));
    }
}
