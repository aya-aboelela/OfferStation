using AutoMapper;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using offerStation.Core.Dtos;
using offerStation.Core.Models;
using System.Linq.Expressions;
using offerStation.Core.Constants;

namespace offerStation.EF.Services
{
    public class AdminAnalysisService:IadminAnalysisService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helperService;
        private readonly IownerAnalysisService _iownerAnalysis;
        private readonly IsupplierAnalysisService _isupplierAnalysis;
        public AdminAnalysisService(IMapper mapper, IUnitOfWork unitOfWork, IHelperService helperService, IownerAnalysisService ownerAnalysis,IsupplierAnalysisService supplierAnalysis)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _helperService = helperService;
            _iownerAnalysis= ownerAnalysis;
            _isupplierAnalysis= supplierAnalysis;

        }
        public async Task<int> getTotalOwners()
        {
            int total =  _unitOfWork.Owners.FindAllAsync(o=>o.IsDeleted==false && o.Approved==true).Result.Count();
            return total;
        }

        public async Task<int> getTotalSupplier()
        {
            int total = _unitOfWork.Suppliers.FindAllAsync(s => s.IsDeleted == false && s.Approved == true).Result.Count();
            return total;
        }
        public async Task<int> getTotalCustomer()
        {
            int total = _unitOfWork.Customers.FindAllAsync(s => s.IsDeleted == false).Result.Count();
            return total;
        }
        public async Task<long> getTotaloffers()
        {
            long total = _unitOfWork.OwnerOffers.FindAllAsync(s => s.IsDeleted == false).Result.Count() + _unitOfWork.SupplierOffers.FindAllAsync(s => s.IsDeleted == false).Result.Count();
            return total;
        }
        public async Task<long> getTotalOrders()
        {
            long total = _unitOfWork.CustomerOrders.FindAllAsync(s => s.IsDeleted == false).Result.Count() + _unitOfWork.OwnerOrders.FindAllAsync(s => s.IsDeleted == false).Result.Count();
            return total;
        }
        public async Task<long> getTotalProducts()
        {
            long total = _unitOfWork.OwnerProducts.FindAllAsync(s => s.IsDeleted == false).Result.Count() + _unitOfWork.SupplierProducts.FindAllAsync(s => s.IsDeleted == false).Result.Count();
            return total;
        }

        public async Task<double> getTotalProfit()
        {
            double sum = 0;

            List<CustomerOrder> customerOrders = (List<CustomerOrder>)await _unitOfWork.CustomerOrders.GetAllAsync();
            List<OwnerOrder> ownerOrders = (List<OwnerOrder>)await _unitOfWork.OwnerOrders.GetAllAsync();
            
            sum += customerOrders.Select(o => o.Total * (Const.Fee / 100)).Sum();
            sum += ownerOrders.Select(o => o.Total * (Const.Fee / 100)).Sum();

            return sum;
        }  
        public async Task<List<customerInfoAnalysis>> getOrderdCustomer()
        {
            List<Customer> customers =(List<Customer>)  await _unitOfWork.Customers.FindAllAsync(c=>c.IsDeleted==false, new List<Expression<Func<Customer, object>>>()
               {
                c=>c.AppUser,
                  c=>c.CustomerOrders
            });
            return  customers.Select(c=>new customerInfoAnalysis { Name=c.AppUser.Name,Phone=c.AppUser.PhoneNumber,ordersCount=c.CustomerOrders.Count()}).ToList();

            
        }
        public async Task<List<customerInfoAnalysis>> getOrderdOwner()
        {
            List<Owner> customers = (List<Owner>)await _unitOfWork.Owners.FindAllAsync(o => o.IsDeleted == false, new List<Expression<Func<Owner, object>>>()
               {
                 o=>o.AppUser,
                  o=>o.OwnerOrders
            });
            return customers.Select(c => new customerInfoAnalysis { Name = c.AppUser.Name, Phone = c.AppUser.PhoneNumber, ordersCount = c.OwnerOrders.Count() }).ToList();


        }

        public async Task<List<AnalysisResult>> getownerSupplierOffersCount()
        {
            AnalysisResult SupplieranalysisResult= new AnalysisResult();
            SupplieranalysisResult.Count=  _unitOfWork.OwnerOrders.FindAllAsync(o=>true, new List<Expression<Func<OwnerOrder, object>>>()
                   {
                     o=>o.Offers,
                }).Result.Select(o=>o.Offers.Count()).Sum();
            SupplieranalysisResult.Name = "supplier Orderd offers";
          
            AnalysisResult ownerResult = new AnalysisResult();
            ownerResult.Count = _unitOfWork.CustomerOrders.FindAllAsync(o => true, new List<Expression<Func<CustomerOrder, object>>>()
                   {
                     o=>o.Offers,
                }).Result.Select(o => o.Offers.Count()).Sum();
            ownerResult.Name = "Owner Orderd offers";

            List<AnalysisResult> results = new List<AnalysisResult>();
            results.Add(ownerResult); 
            results.Add(SupplieranalysisResult);
            return results;

        }

        public async Task<List<AnalysisResult>> getownerSupplierProductCount()
        {
            AnalysisResult SupplieranalysisResult = new AnalysisResult();
            SupplieranalysisResult.Count = _unitOfWork.OwnerOrders.FindAllAsync(o => true, new List<Expression<Func<OwnerOrder, object>>>()
                   {
                     o=>o.Products,
                }).Result.Select(o => o.Products.Count()).Sum();
            SupplieranalysisResult.Name = "supplier Orderd Products";

            AnalysisResult ownerResult = new AnalysisResult();
            ownerResult.Count = _unitOfWork.CustomerOrders.FindAllAsync(o => true, new List<Expression<Func<CustomerOrder, object>>>()
                   {
                     o=>o.Products,
                }).Result.Select(o => o.Products.Count()).Sum();
            ownerResult.Name = "Owner Orderd Products";

            List<AnalysisResult> results = new List<AnalysisResult>();
            results.Add(ownerResult);
            results.Add(SupplieranalysisResult);
            return results;

        }






    }
}
