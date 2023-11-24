using HardwareStore.Dto;
using HardwareStore.Dto.Request;
using HardwareStore.Dto.Response;

namespace HardwareStore.Services.Interfaces;

public interface IInvoiceService
{
    Task<BaseResponseGeneric<int>> AddAsync(string email, InvoiceDtoRequest request);

    Task<BaseResponseGeneric<InvoiceDtoResponse>> FindByIdAsync(int id);
}