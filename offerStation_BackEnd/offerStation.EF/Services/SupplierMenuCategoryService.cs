using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Services
{
    public class SupplierMenuCategoryService : ISupplierMenuCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierMenuCategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<MenuCategoryDetailsDto?> GetMenuCategoryDetails(int id)
        {
            MenuCategoryDetailsDto menuCategoryDto = null;

            SupplierMenuCategory menuCategory = await _unitOfWork.SupplierMenuCategories.GetByIdAsync(id);
            if (menuCategory is not null)
            {
                menuCategoryDto = _mapper.Map<MenuCategoryDetailsDto>(menuCategory);
            }
            return menuCategoryDto;
        }
        public async Task<bool> AddMenuCategory(int SupplierId, MenuCategoryDto menuCategoryDto)
        {
            if (menuCategoryDto is not null)
            {
                SupplierMenuCategory menuCategory = new SupplierMenuCategory();
                menuCategory = _mapper.Map<SupplierMenuCategory>(menuCategoryDto);
                menuCategory.SupplierId = SupplierId;

                _unitOfWork.SupplierMenuCategories.Add(menuCategory);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> EditMenuCategory(int id, MenuCategoryDto menuCategoryDto)
        {
            SupplierMenuCategory menuCategory = await _unitOfWork.SupplierMenuCategories.GetByIdAsync(id);

            if (menuCategory is not null && menuCategoryDto is not null)
            {
                menuCategory.Name = menuCategoryDto.Name;
                menuCategory.Image = menuCategoryDto.Image;

                _unitOfWork.SupplierMenuCategories.Update(menuCategory);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteMenuCategory(int id)
        {
            SupplierMenuCategory menuCategory = await _unitOfWork.SupplierMenuCategories.GetByIdAsync(id);

            if (menuCategory is not null)
            {
                _unitOfWork.SupplierMenuCategories.Delete(menuCategory);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
    }
}
