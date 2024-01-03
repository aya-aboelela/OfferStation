using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using offerStation.Core.Models;
using System.Linq.Expressions;
using offerStation.Core.Constants;

namespace offerStation.EF.Services
{
    public class ownerAnalysisService:IownerAnalysisService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helperService;
        public ownerAnalysisService(IMapper mapper, IUnitOfWork unitOfWork, IHelperService helperService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _helperService = helperService;
        }


       
        public async Task<List<AnalysisResult>> getTop5OwnerOffer(int OwnerId)
        {
            
           List<OwnerOffer> offers=(List<OwnerOffer>)  await  _unitOfWork.OwnerOffers.FindAllAsync(O=>O.OwnerId== OwnerId &&O.IsDeleted==false, new List<Expression<Func<OwnerOffer, object>>>()
               {
                  o=>o.Orders
            }
                );

            List<AnalysisResult> orders = new List<AnalysisResult>();
           offers= offers.Where(O => O.Orders.Count() > 0).ToList().OrderByDescending(o=>o.Orders.Count()).Take(5).ToList();
            offers.ForEach(o =>
            {
                AnalysisResult orderDto = new AnalysisResult();
                orderDto.Count = o.Orders.Count();
                orderDto.Name = o.Name;
                orders.Add(orderDto);
            });
            return orders;


        }
        public async Task<List<AnalysisResult>> getTop5OwnerProduct(int OwnerId)
        {

            List<OwnerProduct> products = (List<OwnerProduct>)await _unitOfWork.OwnerProducts.FindAllAsync(P => P.OwnerId == OwnerId && P.IsDeleted == false, new List<Expression<Func<OwnerProduct, object>>>()
               {
                  o=>o.orders
            }
                );

            List<AnalysisResult> orders = new List<AnalysisResult>();
            products = products.Where(O=>O.orders.Count()>0).ToList().OrderByDescending(o => o.orders.Count()).Take(5).ToList();
            products.ForEach(o =>
            {
                AnalysisResult orderDto = new AnalysisResult();
                orderDto.Count = o.orders.Count();
                orderDto.Name = o.Name;
                orders.Add(orderDto);
            });
            return orders;

        }
        public async Task<int> getOwnerTotalCustomer(int OwnerId)
        {
            List<CustomerOrder> orders= (List<CustomerOrder>) await _unitOfWork.CustomerOrders.FindAllAsync(o=>o.OwnerId == OwnerId);
            return orders.Select(o => o.CustomerId).Distinct().Count();
        }

        public async Task<int> getOwnerTotalOrders(int ownerId)
        {   
            List<CustomerOrder> orders = (List<CustomerOrder>)await _unitOfWork.CustomerOrders.FindAllAsync(o => o.OwnerId == ownerId);
            return orders.Count();
        }

        public async  Task<double> getTotalProfit(int ownerId)
        {
            List<CustomerOrder> orders = (List<CustomerOrder>)await _unitOfWork.CustomerOrders.FindAllAsync(o => o.OwnerId == ownerId);
            return orders.Select(o => (o.Total) - (o.Total * (Const.Fee / 100))).Sum();
        }

        public async Task<int> getProductsCount(int ownerId)
        {
            Owner owner = await _unitOfWork.Owners.FindAsync(o => o.Id == ownerId, new List<Expression<Func<Owner, object>>>()
               {
                  o=>o.OwnerProducts            
            });


            return owner.OwnerProducts.Count();
        }

        public async Task<int> getOffersCount(int ownerId)
        {
            Owner owner = await _unitOfWork.Owners.FindAsync(o => o.Id == ownerId, new List<Expression<Func<Owner, object>>>()
               {
                  o=>o.Offers
            });


            return owner.Offers.Count();
        }


        public async Task overallRating()
        {

        }

        public async Task<AnalysisResult> GetTotalOrdersPneding(int ownerId,OrderStatus status)
        {
            List < CustomerOrder > orders   = (List<CustomerOrder>)  await _unitOfWork.CustomerOrders.FindAllAsync(O => O.OwnerId == ownerId && O.orderStatus == status);
            AnalysisResult result =new AnalysisResult();
            result.Count=orders.Count;
            result.Name = status.ToString()+" Orders";
            return result;


        }
        public async Task<List<AnalysisResult>> getDiffernentOrdersStatus(int ownerId)
        {

            List< AnalysisResult > results=new List<AnalysisResult >();

            AnalysisResult pendingorders = await GetTotalOrdersPneding(ownerId, OrderStatus.pending);

            AnalysisResult shippedorders = await GetTotalOrdersPneding(ownerId, OrderStatus.shipped);

            AnalysisResult deliveradeorders = await GetTotalOrdersPneding(ownerId, OrderStatus.delivered);

            results.Add(pendingorders);

            results.Add(shippedorders);
            results.Add(deliveradeorders);

            return results;



        }

        public async Task<List<AnalysisResult>> ownerOrdersOffersProductCount(int ownerId)
        {
            List<CustomerOrder> orders = (List<CustomerOrder>) await _unitOfWork.CustomerOrders.FindAllAsync(O => O.OwnerId == ownerId , new List<Expression<Func<CustomerOrder, object>>>()
             {
                  O=>O.Offers,
                  O=>O.Products
            });

           long numofOffers=  orders.Select(O => O.Offers.Select(f=>f.Quantity).Sum()).Sum();
           long numofProducts = orders.Select(O => O.Products.Select(f => f.Quantity).Sum()).Sum();

            List<AnalysisResult> results = new List<AnalysisResult>();

            AnalysisResult offersResult = new AnalysisResult();
            offersResult.Count = numofOffers;
            offersResult.Name = "offers";

            AnalysisResult ProductResult = new AnalysisResult();
            ProductResult.Count = numofProducts;
            ProductResult.Name = "products";

            results.Add(ProductResult);
            results.Add(offersResult);
            return results;




        }
        public async Task<List<customerInfoAnalysis>> getTopCustomerInfo(int ownerid)
        {
            List<CustomerOrder> orders = (List < CustomerOrder >) await _unitOfWork.CustomerOrders.FindAllAsync(o => o.OwnerId == ownerid, new List<Expression<Func<CustomerOrder, object>>>()
             {
                  O=>O.Customer.AppUser,
                  
            });
            return orders.GroupBy(o => o.CustomerId).Select(o =>new customerInfoAnalysis { Name= o.Select(o => o.Customer.AppUser.Name).First(),Phone = o.Select(o => o.Customer.AppUser.PhoneNumber).First(), ordersCount= o.Select(o => o.CustomerId).Count() }).OrderByDescending(o=>o.ordersCount).Take(10).ToList();


        }

    }
}
