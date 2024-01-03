using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using offerStation.Core.Constants;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.MappingProfiles;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF
{
    public class AccountService : IAccountService
    {
        //private readonly IMapper _mapper;



        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private ITokenGenerator _tokenGenerator;

        public AccountService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenGenerator tokenGenerator,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public AccountService()
        {
        }

        public async Task<ApiResponse> LoginUser(UserLoginDto dto)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) { return new ApiResponse(404, false, null, "User not found"); }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) { return new ApiResponse(401, false, null, "Invalid password"); }

            int Id = await GetUserTypeId(user);

            return new ApiResponse(200, true, new UserDto
            {
                Name = $"{user.Name}",
                Email = user.Email,
                Token = await _tokenGenerator.GenerateToken(user, Id),
            });
        }

        public async Task<ApiResponse> CustomerRegister(CustomerRegestrationDto customerDto)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(customerDto);
            Customer customer = _mapper.Map<Customer>(customerDto);
            user.Customer = customer;
            customer.AppUser = user;


            user.Addresses = customerDto.Address
                .Select(CAddressd => new Address
                {
                    CityId = CAddressd.CityId,
                    details = CAddressd.details,
                }).ToList();


            IdentityResult result;
            try
            {
                result = await _userManager.CreateAsync(user, customerDto.Password);
            }
            catch (Exception)
            {
                return new ApiResponse(400, false, null, "InValid Inputs");
            }

            if (!result.Succeeded) { return new ApiResponse(400, false, result.Errors); }

            await _userManager.AddToRoleAsync(user, Role.Customer);

            _unitOfWork.Complete();
            _unitOfWork.CommitChanges();

            return new ApiResponse(200, true, new UserDto
            {
                Name = $"{customerDto.Name} ",
                Email = customerDto.Email,
                Token = await _tokenGenerator.GenerateToken(user, customer.Id),
            });
        }

        public async Task<ApiResponse> OwnerRegister(OwnerRegestrationDto ownerDto)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(ownerDto);
            Owner owner = _mapper.Map<Owner>(ownerDto);
            user.Owner = owner;
            owner.AppUser = user;


            user.Addresses = ownerDto.Address
                .Select(OAddressd => new Address
                {
                    CityId = OAddressd.CityId,
                    details = OAddressd.details,
                }).ToList();

            IdentityResult result;
            try
            {
                result = await _userManager.CreateAsync(user, ownerDto.Password);
            }
            catch (Exception)
            {
                return new ApiResponse(400, false, null, "InValid Inputs");
            }

            if (!result.Succeeded) { return new ApiResponse(400, false, result.Errors); }

            await _userManager.AddToRoleAsync(user, Role.Owner);
            _unitOfWork.Complete();
            _unitOfWork.CommitChanges();

            return new ApiResponse(200, true, new UserDto
            {
                Name = $"{ownerDto.Name} ",
                Email = ownerDto.Email,
                Token = await _tokenGenerator.GenerateToken(user, owner.Id),
            });
        }

        public async Task<ApiResponse> SupplierRegister(SupplierRegestrationDto supplierDto)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(supplierDto);
            Supplier supplier = _mapper.Map<Supplier>(supplierDto);
            user.Supplier = supplier;
            supplier.AppUser = user;


            user.Addresses = supplierDto.Address
                .Select(OAddressd => new Address
                {
                    CityId = OAddressd.CityId,
                    details = OAddressd.details,
                }).ToList();

            IdentityResult result;
            try
            {
                result = await _userManager.CreateAsync(user, supplierDto.Password);
            }
            catch (Exception)
            {
                return new ApiResponse(400, false, null, "InValid Inputs");
            }

            if (!result.Succeeded) { return new ApiResponse(400, false, result.Errors); }

            await _userManager.AddToRoleAsync(user, Role.Supplier);

            _unitOfWork.CommitChanges();

            return new ApiResponse(200, true, new UserDto
            {
                Name = $"{supplierDto.Name} ",
                Email = supplierDto.Email,
                Token = await _tokenGenerator.GenerateToken(user, supplier.Id),
            });
        }

       

    private async Task<int> GetUserTypeId(ApplicationUser user)
        {
            int? supplierId = (int?)await _unitOfWork.Suppliers
                .FindWithSelectAsync(s => s.AppUserId == user.Id, s => s.Id);

            if (supplierId != null) { return supplierId.Value; }


            int? OwnerId = (int?)await _unitOfWork.Owners
                .FindWithSelectAsync(o => o.AppUserId == user.Id, o => o.Id);
            
            if (OwnerId != null) { return OwnerId.Value; }

            int? AdminId = (int?)await _unitOfWork.Admins
                .FindWithSelectAsync(o => o.AppUserId == user.Id, o => o.Id);

            if (AdminId != null) { return AdminId.Value; }

            int? CustomerId = (int?)await _unitOfWork.Customers
                .FindWithSelectAsync(o => o.AppUserId == user.Id, o => o.Id);

            return CustomerId.Value;
        }

        //private async Task<byte[]> GetBytes(IFormFile formFile)
        //{
        //    await using var memoryStream = new MemoryStream();
        //    await formFile.CopyToAsync(memoryStream);
        //    return memoryStream.ToArray();
        //}

    }
}
