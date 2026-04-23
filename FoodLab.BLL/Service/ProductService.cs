using SendGrid.Helpers.Errors.Model;
using FoodLab.Shared.DTOs.Products;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace FoodLab.BLL.Services.Products;

public class ProductService(
    IUnitOfWork unitOfWork,
    IBaseValidator<ProductForCreateDto, ProductUpdateDto> validator,
    ILoggerService<Product> logging,
    IMapper mapper)
    : ICrudService<ProductResultDto, ProductForCreateDto, ProductUpdateDto, int, ProductOption>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBaseValidator<ProductForCreateDto, ProductUpdateDto> _validator = validator;
    private readonly ILoggerService<Product> _logging = logging;
    private readonly IMapper _mapper = mapper;

    public async Task<int> AddAsync(ProductForCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var validation = await _validator.CreateValidator(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var mapped = _mapper.Map<Product>(dto);

            return await _unitOfWork.Product.AddAsync(mapped);
        }
        catch (Exception ex)
        {
            _logging.LogAddError(ex);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellation = default)
    {
        try
        {
            var entity = await _unitOfWork.Product.GetByIdAsync(id);

            if (entity is null)
                throw new NotFoundException("Product", id);

            if (entity.IsDeleted)
                throw new GoneException("Product", id);

            entity.IsDeleted = true;

            return await _unitOfWork.SaveAsync(cancellation) > 0;
        }
        catch (Exception ex)
        {
            _logging.LogDeleteError(id, ex);
            throw;
        }
    }

    public async Task<PagedList<ProductForResultDto>> GetAllAsync(ProductOption option, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _unitOfWork.Product
                .GetAll()
                .Where(x => x.IsDeleted == option.IsDeleted);

            if (!query.Any())
                throw new NotFoundException("Product", -1);

            var mapped = query
                .Select(x => _mapper.Map<ProductForResultDto>(x))
                .ToList();

            return new PagedList<ProductForResultDto>(
                mapped,
                mapped.Count,
                option.PageNumber,
                option.PageSize);
        }
        catch (Exception ex)
        {
            _logging.LogError(ex);
            throw;
        }
    }

    public async Task<ProductForResultDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _unitOfWork.Product.GetByIdAsync(id);

            if (entity is null)
                throw new NotFoundException("Product", id);

            return _mapper.Map<ProductForResultDto>(entity);
        }
        catch (Exception ex)
        {
            _logging.LogWarning(id);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(int id, ProductForUpdateDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var validation = await _validator.UpdateValidator(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var entity = await _unitOfWork.Product.GetByIdAsync(id);

            if (entity is null)
                throw new NotFoundException("Product", id);

            _mapper.Map(dto, entity);

            entity.UpdatedAt = DateTime.UtcNow;

            return await _unitOfWork.Product.UpdateAsync(entity);
        }
        catch (Exception ex)
        {
            _logging.LogUpdateError(id, ex);
            throw;
        }
    }
}