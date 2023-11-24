using AutoMapper;
using Microsoft.Extensions.Logging;
using HardwareStore.Dto;
using HardwareStore.Dto.Request;
using HardwareStore.Dto.Response;
using HardwareStore.Entities;
using HardwareStore.Repositories;
using HardwareStore.Services.Interfaces;

namespace HardwareStore.Services.Implementations;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;
    private readonly ILogger<ItemService> _logger;
    private readonly IMapper _mapper;
    private readonly IFileUploader _fileUploader;

    public ItemService(IItemRepository repository, ILogger<ItemService> logger, IMapper mapper, IFileUploader fileUploader)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _fileUploader = fileUploader;
    }

    public async Task<BaseResponsePagination<ItemDtoResponse>> ListAsync(ItemSearch search, CancellationToken cancellationToken = default)
    {
        var response = new BaseResponsePagination<ItemDtoResponse>();

        try
        {
            var tupla = await _repository.ListAsync(predicate: c => c.ItemName.Contains(search.ItemName ?? string.Empty) && c.CategoryId.Equals(search.CategoryId),
                selector: p => _mapper.Map<ItemDtoResponse>(p),
                orderby: p => p.ItemName,
                page: search.Page,
                rows: search.Rows,
                relationships: "Category");

            response.Data = tupla.Collection;
            response.TotalPages = Utilities.GetTotalPages(tupla.Total, search.Rows);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar los Conciertos";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<ItemDtoResponse>>> ListAsyncByCategory(int id)
    {
        var response = new BaseResponseGeneric<ICollection<ItemDtoResponse>>();

        try
        {
            // Codigo
            response.Data = _mapper.Map<ICollection<ItemDtoResponse>>(await _repository.ListAsyncByCategory(id));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar los Articulos";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }


    public async Task<BaseResponseGeneric<ItemDtoRequest>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<ItemDtoRequest>();

        try
        {
            // Codigo
            response.Data = _mapper.Map<ItemDtoRequest>(await _repository.FindByIdAsync(id));
            response.Success = response.Data != null;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al leer el Articulos";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }



    public async Task<BaseResponseGeneric<int>> AddAsync(ItemDtoRequest request)
    {
        var response = new BaseResponseGeneric<int>();

        try
        {
            var categories = _mapper.Map<Items>(request);

            categories.ImageUrl = await _fileUploader.UploadFileAsync(request.Base64Image, request.FileName);
            response.Data = await _repository.AddAsync(categories);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al agregar un Articulo";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }



    public async Task<BaseResponse> UpdateAsync(int id, ItemDtoRequest request)
    {
        var response = new BaseResponse();

        try
        {
            var entity = await _repository.FindByIdAsync(id); // Esto viene con el ChangeTracker
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
            response.ErrorMessage = "Error al actualizar un Concierto";
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