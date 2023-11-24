using HardwareStore.Dto.Request;
using HardwareStore.Dto.Response;
using HardwareStore.Dto;

namespace HardwareStore.Services.Interfaces;

public interface IItemService
{
    Task<BaseResponsePagination<ItemDtoResponse>> ListAsync(ItemSearch search, CancellationToken cancellationToken = default);
    Task<BaseResponseGeneric<ICollection<ItemDtoResponse>>> ListAsyncByCategory(int id);
    Task<BaseResponseGeneric<ItemDtoRequest>> FindByIdAsync(int id);

    Task<BaseResponseGeneric<int>> AddAsync(ItemDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, ItemDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);

}