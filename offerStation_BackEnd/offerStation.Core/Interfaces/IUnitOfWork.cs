using Microsoft.EntityFrameworkCore;
using offerStation.Core.Interfaces.Repositories;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        public IBaseRepository <City> Cities { get; }
        public IBaseRepository <Admin> Admins { get; }
        public IBaseRepository <Address> Addresses { get; }
        public IBaseRepository <Delivery> Deliveries { get; }

        public IBaseRepository <Customer> Customers { get; }
        public IBaseRepository <CustomerCart> CustomerCarts { get; }
        public IBaseRepository <CustomerOrder> CustomerOrders { get; }
        public IBaseRepository <CustomerReview> CustomerReviews { get; }
        public IBaseRepository <CustomerCartProduct> CustomerCartProducts { get; }
        public IBaseRepository <CustomerCartOffer> CustomerCartOffers { get; }
        public IBaseRepository <CustomerOrderOffer> CustomerOrderOffers { get; }
        public IBaseRepository <CustomerCardDetails> CustomerCardDetails { get; }
        public IBaseRepository <CustomerOrderProduct> CustomerOrderProducts { get; }
        public IBaseRepository <CustomerOrderDelivery> CustomerOrderDeliveries { get; }

        public IBaseRepository <Owner> Owners { get;  }
        public IBaseRepository <OwnerCart> OwnerCarts { get; }
        public IBaseRepository <OwnerOrder> OwnerOrders { get;  }
        public IBaseRepository <OwnerOffer> OwnerOffers { get;  }
        public IBaseRepository <OwnerProduct> OwnerProducts { get;  }
        public IBaseRepository <OwnerReview> OwnerReviews { get;  }
        public IBaseRepository <OwnerCategory> OwnerCategories { get;  }
        public IBaseRepository <OwnerCardDetails> OwnerCardDetails { get; }
        public IBaseRepository <OwnerCartProduct> OwnerCartProducts { get; }
        public IBaseRepository <OwnerCartOffer> OwnerCartOffers { get; }
        public IBaseRepository <OwnerMenuCategory> OwnerMenuCategories { get; }
        public IBaseRepository <OwnerOrderProduct> OwnerOrderProducts { get; }
        public IBaseRepository <OwnerOfferProduct> OwnerOfferProducts { get; }
        public IBaseRepository <OwnerOrderDelivery> OwnerOrderDeliveries { get; }
        
        public IBaseRepository <Supplier> Suppliers { get;  }
        public IBaseRepository <SupplierOffer> SupplierOffers { get; }
        public IBaseRepository <SupplierProduct> SupplierProducts { get; }
        public IBaseRepository <SupplierCategory> SupplierCategories { get; }
        public IBaseRepository <SupplierOfferProduct> SupplierOfferProducts { get; }
        public IBaseRepository <SupplierMenuCategory> SupplierMenuCategories { get; }

        int Complete();
        void CommitChanges();
    }
}
