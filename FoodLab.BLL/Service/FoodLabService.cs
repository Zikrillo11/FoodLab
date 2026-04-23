using Abp.Domain.Uow;
using FluentValidation;
using FoodLab.BLL.Interfaces.Base;
using FoodLab.BLL.Services.Logging;
using FoodLab.BLL.Validations;
using FoodLab.DAL.Interfaces;
using FoodLab.Domain.Entities;
using FoodLab.Shared.Common.Exceptions;
using FoodLab.Shared.Common.Pagination;
using FoodLab.Shared.DTOs.Product;
using FoodLab.Shared.Options.Product;
using PagedList;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;

namespace FoodLab.BLL.Services.Products;

public class ProductService(
    IUnitOfWork unitOfWork,
    IBaseValidator<ProductForCreateDto, ProductUpdateDto> validator,
    ILoggerService<Product> logging)
    : ICrudService<ProductResultDto, ProductForCreateDto, ProductUpdateDto, int, ProductOption>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBaseValidator<ProductForCreateDto, ProductUpdateDto> _validator = validator;
    private readonly ILoggerService<Product> _logging = logging;

    public async Task<int> AddAsync(ProductForCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var validation = await _validator.CreateValidator(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var entity = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            return await _unitOfWork.Product.AddAsync(entity);
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

    public async Task<PagedList<ProductResultDto>> GetAllAsync(ProductOption option, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _unitOfWork.Product
                .GetAll()
                .Where(x => x.IsDeleted == option.IsDeleted);

            if (!query.Any())
                throw new NotFoundException("Product", -1);

            var list = query.ToList();

            var result = list.Select(x => new ProductResultDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                CategoryName = x.Category != null ? x.Category.Name : ""
            }).ToList();

            return new PagedList<ProductResultDto>(
                result,
                result.Count,
                option.PageNumber,
                option.PageSize);
        }
        catch (Exception ex)
        {
            _logging.LogError(ex);
            throw;
        }
    }

    public async Task<ProductResultDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _unitOfWork.Product.GetByIdAsync(id);

            if (entity is null)
                throw new NotFoundException("Product", id);

            if (entity.IsDeleted)
                throw new GoneException("Product", id);

            return new ProductResultDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CategoryName = entity.Category != null ? entity.Category.Name : ""
            };
        }
        catch (Exception ex)
        {
            _logging.LogWarning(id);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var validation = await _validator.UpdateValidator(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var entity = await _unitOfWork.Product.GetByIdAsync(id);

            if (entity is null)
                throw new NotFoundException("Product", id);

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.CategoryId = dto.CategoryId;
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