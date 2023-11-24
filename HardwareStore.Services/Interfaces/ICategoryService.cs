using HardwareStore.Dto.Request;
using HardwareStore.Dto.Response;
using HardwareStore.Dto;

namespace HardwareStore.Services.Interfaces;

public interface ICategoryService
{
    Task<BaseResponseGeneric<ICollection<CategoryDtoResponse>>> ListAsync();

    Task<BaseResponseGeneric<CategoryDtoResponse>> FindByIdAsync(int id);

    Task<BaseResponseGeneric<int>> AddAsync(CategoryDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, CategoryDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);
}