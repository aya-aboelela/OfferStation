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
    public class OwnerMenuCategoryService : IOwnerMenuCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerMenuCategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<MenuCategoryDetailsDto?> GetMenuCategoryDetails(int id)
        {
            MenuCategoryDetailsDto menuCategoryDto = null;

            OwnerMenuCategory menuCategory = await _unitOfWork.OwnerMenuCategories.GetByIdAsync(id);
            if (menuCategory is not null)
            {
                menuCategoryDto = _mapper.Map<MenuCategoryDetailsDto>(menuCategory);
            }
            return menuCategoryDto;
        }
        public async Task<bool> AddMenuCategory(int ownerId, MenuCategoryDto menuCategoryDto)
        {
            if (menuCategoryDto is not null)
            {
                OwnerMenuCategory menuCategory = new OwnerMenuCategory();
                menuCategory = _mapper.Map<OwnerMenuCategory>(menuCategoryDto);
                menuCategory.OwnerId = ownerId;

                _unitOfWork.OwnerMenuCategories.Add(menuCategory);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> EditMenuCategory(int id, MenuCategoryDto menuCategoryDto)
        {
            OwnerMenuCategory menuCategory = await _unitOfWork.OwnerMenuCategories.GetByIdAsync(id);

            if (menuCategory is not null && menuCategoryDto is not null)
            {
                menuCategory.Name = menuCategoryDto.Name;
                menuCategory.Image = menuCategoryDto.Image;

                _unitOfWork.OwnerMenuCategories.Update(menuCategory);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteMenuCategory(int id)
        {
            OwnerMenuCategory menuCategory = await _unitOfWork.OwnerMenuCategories.GetByIdAsync(id);

            if (menuCategory is not null)
            {
                _unitOfWork.OwnerMenuCategories.Delete(menuCategory);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
    }
}
