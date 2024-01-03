using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helperService;

        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork, IHelperService helperService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _helperService = helperService;
        }

        public async Task<CustomerInfoDto?> GetCustomer(int id)
        {

            CustomerInfoDto customerInfoDto = null;

            Customer? customer = await _unitOfWork.Customers.FindAsync(c => c.Id == id && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.AppUser,
                }); 

            if (customer is not null)
            {
                customerInfoDto = new CustomerInfoDto();
                customerInfoDto = _mapper.Map<CustomerInfoDto>(customer);
            }
            return customerInfoDto;
        }
        public async Task<bool> EditCustomer(int id, CustomerInfoDto customerInfoDto)
        {
            Customer customer = await _unitOfWork.Customers.FindAsync(c => c.Id == id && !c.IsDeleted, 
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.AppUser
                });

            if (customer is not null)
            {
                customer.AppUser.Name = customerInfoDto.Name;
                customer.AppUser.Email = customerInfoDto.Email;
                customer.AppUser.UserName = customerInfoDto.Name;
                customer.AppUser.PhoneNumber = customerInfoDto.PhoneNumber;

                _unitOfWork.Customers.Update(customer);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> SuspendCustomer(int id)
        {
            Customer customer = await _unitOfWork.Customers.GetByIdAsync(id);
            
            if (customer is not null)
            {
                _unitOfWork.Customers.Delete(customer);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> RemoveCustomerSuspension(int id)
        {
            Customer customer = await _unitOfWork.Customers.GetByIdAsync(id);
            
            if (customer is not null)
            {
                customer.IsDeleted = false;

                _unitOfWork.Customers.Update(customer);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<List<ReviewDto>?> GetAllCustomersReviews()
        {
            List<ReviewDto> reviewListDto = null;

            IEnumerable<CustomerReview> reviewList = await _unitOfWork.CustomerReviews
                .FindAllAsync(r => !r.IsDeleted, 
                new List<Expression<Func<CustomerReview, object>>>()
                {
                    r => r.Customer.AppUser,
                });

            if(reviewList is not null)
            {
                reviewListDto = _mapper.Map<List<ReviewDto>>(reviewList);
            }
            return reviewListDto;
        }
        public async Task<bool> AddReview(int customerId, int ownerId, ReviewInfoDto reviewDto)
        {
            if (reviewDto is not null)
            {
                CustomerReview review = new CustomerReview();
                review = _mapper.Map<CustomerReview>(reviewDto);

                review.OwnerId = ownerId;
                review.CustomerId = customerId;

                _unitOfWork.CustomerReviews.Add(review);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteReview(int id)
        {
            CustomerReview review = await _unitOfWork.CustomerReviews.GetByIdAsync(id);

            if(review is not null)
            {
                _unitOfWork.CustomerReviews.Delete(review);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
    }
}
