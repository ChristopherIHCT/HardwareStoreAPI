using AutoMapper;
using Microsoft.Extensions.Logging;
using HardwareStore.Dto;
using HardwareStore.Dto.Request;
using HardwareStore.Dto.Response;
using HardwareStore.Entities;
using HardwareStore.Repositories;
using HardwareStore.Services.Interfaces;
namespace HardwareStore.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<CategoryService> _logger;
    private readonly IMapper _mapper;
    private readonly IFileUploader _fileUploader;

    public CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger, IMapper mapper, IFileUploader fileUploader)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _fileUploader = fileUploader;
    }

    public async Task<BaseResponseGeneric<ICollection<CategoryDtoResponse>>> ListAsync()
    {
        var response = new BaseResponseGeneric<ICollection<CategoryDtoResponse>>();

        try
        {
            // Codigo
            response.Data = _mapper.Map<ICollection<CategoryDtoResponse>>(await _repository.ListAsync());
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar las Categorias";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponseGeneric<CategoryDtoResponse>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<CategoryDtoResponse>();

        try
        {
            // Codigo
            response.Data = _mapper.Map<CategoryDtoResponse>(await _repository.FindByIdAsync(id));
            response.Success = response.Data != null;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al leer la Categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponseGeneric<int>> AddAsync(CategoryDtoRequest request)
    {
        var response = new BaseResponseGeneric<int>();

        try
        {
            var category = _mapper.Map<Category>(request);

            category.ImageUrl = await _fileUploader.UploadFileAsync(request.Base64Image, request.FileName);
            response.Data = await _repository.AddAsync(category);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al agregar un Categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponse> UpdateAsync(int id, CategoryDtoRequest request)
    {
        var response = new BaseResponse();

        try
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity is null)
            {
                response.ErrorMessage = "El registro no se encuentra";
                return response;
            }

            _mapper.Map(request, entity);

            if (!string.IsNullOrEmpty(request.Base64Image))
            {
                entity.ImageUrl = await _fileUploader.UploadFileAsync(request.Base64Image, request.FileName);
            }

            await _repository.UpdateAsync();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al actualizar Categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();

        try
        {
            // Codigo
            await _repository.DeleteAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al eliminar";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }
}