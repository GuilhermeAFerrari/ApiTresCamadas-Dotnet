using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevIO.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : MainController
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;

    public ProductsController(
        IMapper mapper,
        IProductRepository productRepository,
        IProductService productService,
        INotifier notifier) : base(notifier)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsSuppliersAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductViewModel>> GetByIdAsync(Guid id)
    {
        var productViewModel = await GetProductAsync(id);
        if (productViewModel == null) return NotFound();
        return productViewModel;
    }

    [HttpPost]
    public async Task<ActionResult<ProductViewModel>> AddAsync(ProductViewModel productViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _productService.AddAsync(_mapper.Map<Product>(productViewModel));

        return CustomResponse(HttpStatusCode.Created, productViewModel);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, ProductViewModel productViewModel)
    {
        if (id != productViewModel.Id)
        {
            NotifyError("The ids entered are not the same");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var productUpdate = await GetProductAsync(id);

        productUpdate.SupplierId = productViewModel.SupplierId;
        productUpdate.Name = productViewModel.Name;
        productUpdate.Description = productViewModel.Description;
        productUpdate.Value = productViewModel.Value;
        productUpdate.Active = productViewModel.Active;

        await _productService.UpdateAsync(_mapper.Map<Product>(productUpdate));

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProductViewModel>> RemoveAsync(Guid id)
    {
        var product = await GetProductAsync(id);

        if (product == null) return NotFound();

        await _productService.RemoveAsync(id);
        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<ProductViewModel> GetProductAsync(Guid id)
    {
        return _mapper.Map<ProductViewModel>(await _productRepository.GetProductSupplierAsync(id));
    }
}