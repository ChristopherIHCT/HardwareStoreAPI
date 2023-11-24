using AutoMapper;
using HardwareStore.Dto;
using HardwareStore.Dto.Request;
using HardwareStore.Dto.Response;
using HardwareStore.Entities;
using HardwareStore.Repositories;
using Microsoft.Extensions.Logging;
using HardwareStore.Services.Interfaces;

namespace HardwareStore.Services.Implementations;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repository;
    private readonly ILogger<InvoiceService> _logger;
    private readonly IMapper _mapper;
    private readonly IItemRepository _itemRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IInvoiceDetailsRepository _invoiceDetailsRepository;
    
    public InvoiceService(IInvoiceRepository repository, ILogger<InvoiceService> logger, IMapper mapper, IItemRepository itemRepository,
        ICustomerRepository customerRepository, IInvoiceDetailsRepository invoiceDetails)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _itemRepository = itemRepository;
        _customerRepository = customerRepository;
        _invoiceDetailsRepository = invoiceDetails;
    }

    public async Task<BaseResponseGeneric<int>> AddAsync(string email, InvoiceDtoRequest request)
    {
        var response = new BaseResponseGeneric<int>();
        double totalAmount = 0;

        try
        {
            await _repository.CreateTransactionAsync();
            var customer = await _customerRepository.FindByEmailAsync(email);
            if (customer is null)
            {
                throw new InvalidOperationException($"La cuenta {email} no está registrada como Cliente");
            }

            var newInvoice = new Invoices
            {
                CustomerId = customer.Id,
                SaleDate = request.SaleDate,
                DocNum = request.DocNum,
                Total = 0 // Se establece temporalmente en 0
            };

            var invoiceId = await _repository.AddAsync(newInvoice);
            await _repository.UpdateAsync();
            // Obtener el ID de la factura después de agregarla
            if (invoiceId > 0)
            {
                
                foreach (var detail in request.invoiceDetails)
                {
                    var item = await _itemRepository.FindByIdAsync(detail.ItemId);
                    if (item is not null)
                    {
                        if (item.Stock < detail.Quantity)
                        {
                            throw new InvalidOperationException($"No hay suficiente stock para el artículo con ID {detail.ItemId}");
                        }
                        var itemTotal = item.Price * detail.Quantity;
                        totalAmount += itemTotal;

                        var invoiceDetail = new InvoiceDetails
                        {
                            InvoiceId = invoiceId,
                            ItemId = detail.ItemId,
                            Quantity = detail.Quantity,
                        };
                

                        await _invoiceDetailsRepository.AddAsync(invoiceDetail);
                        item.Stock -= detail.Quantity;
                        await _itemRepository.UpdateAsync(item.Id, item);
                    }
                   
                }
              
                // Actualizar el total de la factura con el monto calculado
                newInvoice.Total = totalAmount;

                // Actualizar la factura con el total calculado
                await _repository.UpdateAsync(newInvoice.Id, newInvoice);

                response.Data = invoiceId;
                response.Success = true;

                _logger.LogInformation("Se creó correctamente la venta para {email}", email);
            }
        }
        catch (InvalidOperationException ex)
        {
            await _repository.RollBackAsync();
            response.ErrorMessage = ex.Message;
            _logger.LogWarning(ex, "{ErrorMessage}", response.ErrorMessage);
        }
        catch (Exception ex)
        {
            await _repository.RollBackAsync();
            response.ErrorMessage = "Error al crear la venta";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<InvoiceDtoResponse>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<InvoiceDtoResponse>();

        try
        {
            // Codigo
            var Invoice = await _repository.FindByIdAsync(id);
            response.Data = _mapper.Map<InvoiceDtoResponse>(Invoice);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al seleccionar la venta";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }
}