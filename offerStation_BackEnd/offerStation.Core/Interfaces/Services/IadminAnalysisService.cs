using offerStation.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces.Services
{
    public interface IadminAnalysisService
    {
        Task<int> getTotalOwners();
        Task<int> getTotalSupplier();
        Task<long> getTotaloffers();
        Task<long> getTotalProducts();
        Task<double> getTotalProfit();
        Task<long> getTotalOrders();
        Task<List<customerInfoAnalysis>> getOrderdCustomer();
        Task<List<customerInfoAnalysis>> getOrderdOwner();
        Task<int> getTotalCustomer();
        Task<List<AnalysisResult>> getownerSupplierOffersCount();
        Task<List<AnalysisResult>> getownerSupplierProductCount();

    }
}
