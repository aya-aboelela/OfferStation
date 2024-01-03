using AutoMapper;
using offerStation.Core.Constants;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Interfaces;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Services
{
    public class SupplierAnalysisService:IsupplierAnalysisService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helperService;
        public SupplierAnalysisService(IMapper mapper, IUnitOfWork unitOfWork, IHelperService helperService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _helperService = helperService;
        }



        public async Task<List<AnalysisResult>> getTop5Offer(int supplierId)
        {

            List<SupplierOffer> offers = (List<SupplierOffer>)await _unitOfWork.SupplierOffers.FindAllAsync(O => O.SupplierID == supplierId && O.IsDeleted == false, new List<Expression<Func<SupplierOffer, object>>>()
               {
                  o=>o.orders
            }
                 );

            List<AnalysisResult> orders = new List<AnalysisResult>();
            offers = offers.Where(O => O.orders.Count() > 0).ToList().OrderByDescending(o => o.orders.Count()).Take(5).ToList();
            offers.ForEach(o =>
            {
                AnalysisResult orderDto = new AnalysisResult();
                orderDto.Count = o.orders.Count();
                orderDto.Name = o.Name;
                orders.Add(orderDto);
            });
            return orders;


        }
        public async Task<List<AnalysisResult>> getTop5Product(int supplierId)
        {

            List<SupplierProduct> products = (List<SupplierProduct>)await _unitOfWork.SupplierProducts.FindAllAsync(P => P.SupplierId == supplierId && P.IsDeleted == false, new List<Expression<Func<SupplierProduct, object>>>()
               {
                  o=>o.orders
            }
                );

            List<AnalysisResult> orders = new List<AnalysisResult>();
            products = products.Where(O => O.orders.Count() > 0).ToList().OrderByDescending(o => o.orders.Count()).Take(5).ToList();
            products.ForEach(o =>
            {
                AnalysisResult orderDto = new AnalysisResult();
                orderDto.Count = o.orders.Count();
                orderDto.Name = o.Name;
                orders.Add(orderDto);
            });
            return orders;

        }
        public async Task<int> getTotalCustomer(int supplierId)
        {
            List<OwnerOrder> orders = (List<OwnerOrder>)await _unitOfWork.OwnerOrders.FindAllAsync(o => o.SupplierId == supplierId);
            return orders.Select(o => o.OwnerId).Distinct().Count();
        }

        public async Task<int> getTotalOrders(int supplierId)
        {
            List<OwnerOrder> orders = (List<OwnerOrder>)await _unitOfWork.OwnerOrders.FindAllAsync(o => o.SupplierId == supplierId);
            return orders.Count();
        }

        public async Task<double> getTotalProfit(int supplierId)
        {
            List<OwnerOrder> orders = (List<OwnerOrder>)await _unitOfWork.OwnerOrders.FindAllAsync(o => o.SupplierId == supplierId);
            return orders.Select(o => (o.Total) - (o.Total * (Const.Fee / 100))).Sum(); 
        }

        public async Task<int> getProductsCount(int supplierId)
        {
           

            List <SupplierProduct> products = (List<SupplierProduct>) await _unitOfWork.SupplierProducts.FindAllAsync(p=>p.SupplierId==supplierId && p.IsDeleted ==false);

            return products.Count();
        }

        public async Task<int> getOffersCount(int supplierId)
        {
            List<SupplierOffer> offers = (List<SupplierOffer>)await _unitOfWork.SupplierOffers.FindAllAsync(o =>o.SupplierID  == supplierId && o.IsDeleted == false);

            return offers.Count();
        }


        public async Task overallRating()
        {

        }

        public async Task<AnalysisResult> GetTotalOrdersStatus(int supplierId, OrderStatus status)
        {
            List<OwnerOrder> orders = (List<OwnerOrder>)await _unitOfWork.OwnerOrders.FindAllAsync(O => O.SupplierId == supplierId && O.orderStatus == status);
            AnalysisResult result = new AnalysisResult();
            result.Count = orders.Count;
            result.Name = status.ToString() + " Orders";
            return result;


        }
        public async Task<List<AnalysisResult>> getDiffernentOrdersStatus(int supplierId)
        {

            List<AnalysisResult> results = new List<AnalysisResult>();

            AnalysisResult pendingorders = await GetTotalOrdersStatus(supplierId, OrderStatus.pending);

            AnalysisResult shippedorders = await GetTotalOrdersStatus(supplierId, OrderStatus.shipped);

            AnalysisResult deliveradeorders = await GetTotalOrdersStatus(supplierId, OrderStatus.delivered);
                
            results.Add(pendingorders);

            results.Add(shippedorders);
            results.Add(deliveradeorders);

            return results;



        }

        public async Task<List<AnalysisResult>> getOrdersOffersProductCount(int supplierId)
        {
            List<OwnerOrder> orders = (List<OwnerOrder>)await _unitOfWork.OwnerOrders.FindAllAsync(O => O.SupplierId == supplierId, new List<Expression<Func<OwnerOrder, object>>>()
             {
                  O=>O.Offers,
                  O=>O.Products
            });

            long numofOffers = orders.Select(O => O.Offers.Select(f => f.Quantity).Sum()).Sum();
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
        public async Task<List<customerInfoAnalysis>> getTopCustomerInfo(int supplierId)
        {
            List<OwnerOrder> orders = (List<OwnerOrder>)await _unitOfWork.OwnerOrders.FindAllAsync(o => o.SupplierId == supplierId, new List<Expression<Func<OwnerOrder, object>>>()
             {
                  O=>O.Owner.AppUser,

            });
            return orders.GroupBy(o => o.OwnerId).Select(o => new customerInfoAnalysis { Name = o.Select(o => o.Owner.AppUser.Name).First(), Phone = o.Select(o => o.Owner.AppUser.PhoneNumber).First(), ordersCount = o.Select(o => o.OwnerId).Count() }).OrderByDescending(o => o.ordersCount).Take(10).ToList();


        }

    }
}
