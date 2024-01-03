using Microsoft.EntityFrameworkCore;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Repositories;
using offerStation.Core.Models;
using offerStation.EF.Data;
using offerStation.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository <City> Cities { get; private set; }
        public IBaseRepository <Admin> Admins { get; private set; }
        public IBaseRepository <Address> Addresses { get; private set; } 
        public IBaseRepository <Delivery> Deliveries { get; private set; }

        public IBaseRepository <Customer> Customers { get; private set; }
        public IBaseRepository <CustomerCart> CustomerCarts { get; private set; }
        public IBaseRepository <CustomerOrder> CustomerOrders { get; private set; }
        public IBaseRepository <CustomerReview> CustomerReviews { get; private set; }
        public IBaseRepository <CustomerCartProduct> CustomerCartProducts { get; private set; }
        public IBaseRepository <CustomerCartOffer> CustomerCartOffers { get; private set; }
        public IBaseRepository <CustomerOrderOffer> CustomerOrderOffers { get; private set; }
        public IBaseRepository <CustomerCardDetails> CustomerCardDetails { get; private set; }
        public IBaseRepository <CustomerOrderProduct> CustomerOrderProducts { get; private set; }
        public IBaseRepository <CustomerOrderDelivery> CustomerOrderDeliveries { get; private set; }

        public IBaseRepository <Owner> Owners { get; private set; }
        public IBaseRepository <OwnerCart> OwnerCarts { get; private set; }
        public IBaseRepository <OwnerOrder> OwnerOrders { get; private set; }
        public IBaseRepository <OwnerOffer> OwnerOffers { get; private set; }
        public IBaseRepository <OwnerReview> OwnerReviews { get; private set; }
        public IBaseRepository <OwnerProduct> OwnerProducts { get; private set; }
        public IBaseRepository <OwnerCategory> OwnerCategories { get; private set; }
        public IBaseRepository <OwnerCardDetails> OwnerCardDetails { get; private set; }
        public IBaseRepository <OwnerCartProduct> OwnerCartProducts { get; private set; }
        public IBaseRepository <OwnerCartOffer> OwnerCartOffers { get; private set; }
        public IBaseRepository <OwnerMenuCategory> OwnerMenuCategories { get; private set; }
        public IBaseRepository <OwnerOrderProduct> OwnerOrderProducts { get; private set; }
        public IBaseRepository <OwnerOfferProduct> OwnerOfferProducts { get; private set; }
        public IBaseRepository <OwnerOrderDelivery> OwnerOrderDeliveries { get; private set; }

        public IBaseRepository<Supplier> Suppliers { get; private set; }
        public IBaseRepository<SupplierOffer> SupplierOffers { get; private set; }
        public IBaseRepository<SupplierProduct> SupplierProducts { get; private set; }
        public IBaseRepository<SupplierCategory> SupplierCategories { get; private set; }
        public IBaseRepository<SupplierMenuCategory> SupplierMenuCategories { get; private set; }
        public IBaseRepository<SupplierOfferProduct> SupplierOfferProducts { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            BeginTransaction();

            Admins = new BaseRepository<Admin>(_context);
            Cities = new BaseRepository<City>(_context);
            Addresses = new BaseRepository<Address>(_context);
            Deliveries = new BaseRepository<Delivery>(_context);

            Customers = new BaseRepository<Customer>(_context);
            CustomerCarts = new BaseRepository<CustomerCart>(_context);
            CustomerOrders = new BaseRepository<CustomerOrder>(_context);
            CustomerReviews = new BaseRepository<CustomerReview>(_context);
            CustomerCartOffers = new BaseRepository<CustomerCartOffer>(_context);
            CustomerOrderOffers = new BaseRepository<CustomerOrderOffer>(_context);
            CustomerCardDetails = new BaseRepository<CustomerCardDetails>(_context);
            CustomerCartProducts = new BaseRepository<CustomerCartProduct>(_context);
            CustomerOrderProducts = new BaseRepository<CustomerOrderProduct>(_context);
            CustomerOrderDeliveries = new BaseRepository<CustomerOrderDelivery>(_context);

            Owners = new BaseRepository<Owner>(_context);
            OwnerCarts = new BaseRepository<OwnerCart>(_context);
            OwnerOrders = new BaseRepository<OwnerOrder>(_context);
            OwnerOffers = new BaseRepository<OwnerOffer>(_context);
            OwnerReviews = new BaseRepository<OwnerReview>(_context);
            OwnerProducts = new BaseRepository<OwnerProduct>(_context);
            OwnerCategories = new BaseRepository<OwnerCategory>(_context);
            OwnerCardDetails = new BaseRepository<OwnerCardDetails>(_context);
            OwnerCartProducts = new BaseRepository<OwnerCartProduct>(_context);
            OwnerCartProducts = new BaseRepository<OwnerCartProduct>(_context);
            OwnerCartOffers = new BaseRepository<OwnerCartOffer>(_context);
            OwnerOrderProducts = new BaseRepository<OwnerOrderProduct>(_context);
            OwnerOfferProducts = new BaseRepository<OwnerOfferProduct>(_context);
            OwnerMenuCategories = new BaseRepository<OwnerMenuCategory>(_context);
            OwnerOrderDeliveries = new BaseRepository<OwnerOrderDelivery>(_context);

            Suppliers = new BaseRepository<Supplier>(_context);
            SupplierOffers = new BaseRepository<SupplierOffer>(_context);
            SupplierProducts = new BaseRepository<SupplierProduct>(_context);
            SupplierCategories = new BaseRepository<SupplierCategory>(_context);
            SupplierOfferProducts = new BaseRepository<SupplierOfferProduct>(_context);
            SupplierMenuCategories = new BaseRepository<SupplierMenuCategory>(_context);
        }

        private void BeginTransaction()
        {
            if(_context.Database.CurrentTransaction is null)
            {
                _context.Database.BeginTransaction();   
            }
        }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch
            {
                _context.Database.CurrentTransaction.Rollback();             
                return 0;
            }
        }
        public void CommitChanges()
        {
            try
            {
                _context.SaveChanges();
                _context.Database.CurrentTransaction.Commit();
            }
            catch
            {
                _context.Database.CurrentTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            CommitChanges();
            _context.Dispose();
        }
    }
}
