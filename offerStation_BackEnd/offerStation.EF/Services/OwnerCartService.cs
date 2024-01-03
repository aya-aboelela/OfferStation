using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using offerStation.Core.Constants;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.MappingProfiles;
using offerStation.Core.Models;
using offerStation.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF
{
    public class OwnerCartService : IOwnerCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public OwnerCartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetCartDetails(int userIdentifier)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            if (owner.OwnerCart is null)
            {
                return new ApiResponse(200, true, null, "Cart Is Empty!");
            }

            OwnerCartDto result;
            string supplierName = "";
            int productsCount = owner.OwnerCart.Products.Count();
            int offersCount = owner.OwnerCart.Offers.Count();
            if (productsCount > 0 || offersCount > 0)
            {
                if (productsCount > 0)
                {
                    int currentId = owner.OwnerCart.Products[0].Id;

                    OwnerProduct sp = await _unitOfWork.OwnerProducts.FindAsync(p => p.Id == currentId,
                    new List<Expression<Func<OwnerProduct, object>>>()
                    {
                    p => p.Owner.AppUser,
                    });
                    supplierName = sp.Owner.AppUser.Name;
                }
                else
                {
                    int currentId = owner.OwnerCart.Offers[0].Id;

                    OwnerOffer sf = await _unitOfWork.OwnerOffers.FindAsync(o => o.Id == currentId,
                    new List<Expression<Func<OwnerOffer, object>>>()
                    {
                    o => o.Owner.AppUser,
                    });
                    supplierName = sf.Owner.AppUser.Name;
                }
            }
            else
            {
                return new ApiResponse(200, true, null, "Cart Is Empty!");
            }
            result = _mapper.Map<OwnerCartDto>(owner.OwnerCart);

            result.SupplierName = supplierName;

            return new ApiResponse(200, true, result, "Cart Info");
        }

        public async Task<ApiResponse> AddProductToCart(int userIdentifier, ProductDetailsDto Product)
        {
            Owner owner = await _unitOfWork.Owners 
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted, 
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if(owner is null)
                return new ApiResponse(401, false);

            SupplierProduct sp = await _unitOfWork.SupplierProducts.FindAsync(p => p.Id == Product.Id,
                new List<Expression<Func<SupplierProduct, object>>>()
                {
                    p =>p.Supplier.AppUser,
                });

            if(owner.OwnerCart is null)
            {
                OwnerCart cart = new() { OwnerId = userIdentifier , SupplierId = sp.SupplierId };

                _unitOfWork.OwnerCarts.Add(cart);

                owner.OwnerCart = cart;

                _unitOfWork.Complete();
            }

            OwnerCartDto result;

            if (owner.OwnerCart.OwnerId != sp.SupplierId)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place !");

            if (owner.OwnerCart.Products is null)
            {
                OwnerCartProduct ownerCartProduct = new ()
                {
                    SupplierProductId = sp.Id,
                    CartId = owner.OwnerCart.Id,
                    Quantity = 1,
                    Price = sp.Price,
                    Total = sp.Price
                };

                _unitOfWork.OwnerCartProducts.Add(ownerCartProduct);
                _unitOfWork.Complete();

                List<OwnerCartProduct> ownerCartProductsList = new List<OwnerCartProduct>();
                ownerCartProductsList.Add(ownerCartProduct);

                owner.OwnerCart.Products = ownerCartProductsList;
            }
            else
            {
                OwnerCartProduct ocp = owner.OwnerCart.Products.FirstOrDefault(p => p.SupplierProductId == sp.Id);
                if (ocp is null)
                    owner.OwnerCart.Products.Add(new OwnerCartProduct { SupplierProductId = sp.Id, SupplierProduct = sp, Quantity = 1, Price = sp.Price,Total = sp.Price });
                else
                {
                    ocp.Quantity++;
                    ocp.Total += sp.Price;
                }
            }
            
            owner.OwnerCart.Total += sp.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<OwnerCartDto>(owner.OwnerCart);
            
            result.SupplierName = sp.Supplier.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }

        public async Task<ApiResponse> AddOfferToCart(int userIdentifier, ProductDetailsDto Offer)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            SupplierOffer sf = await _unitOfWork.SupplierOffers.FindAsync(o => o.Id == Offer.Id,
                new List<Expression<Func<SupplierOffer, object>>>()
                {
                    o =>o.Supplier.AppUser,
                });

            if (owner.OwnerCart is null)
            {
                OwnerCart cart = new() { OwnerId = userIdentifier, SupplierId = sf.SupplierID };

                _unitOfWork.OwnerCarts.Add(cart);

                owner.OwnerCart = cart;

                _unitOfWork.Complete();
            }
            OwnerCartDto result;

            if (owner.OwnerCart.OwnerId != sf.SupplierID)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place !");

            if (owner.OwnerCart.Offers is null)
            {
                OwnerCartOffer ownerCartOffer = new()
                {
                    SupplierOffertId = sf.Id,
                    CartId = owner.OwnerCart.Id,
                    Quantity = 1,
                    Price = sf.Price,
                    Total = sf.Price
                };
                _unitOfWork.OwnerCartOffers.Add(ownerCartOffer);
                _unitOfWork.Complete();

                List<OwnerCartOffer> ownerCartProductsList = new List<OwnerCartOffer>();
                ownerCartProductsList.Add(ownerCartOffer);

                owner.OwnerCart.Offers = ownerCartProductsList;

            }
            else
            {
                OwnerCartOffer cco = owner.OwnerCart.Offers.FirstOrDefault(o => o.SupplierOffertId == sf.Id);
                if (cco is null)
                    owner.OwnerCart.Offers.Add(new OwnerCartOffer { SupplierOffertId = sf.Id, SupplierOffer = sf, Quantity = 1,Price = sf.Price,Total = sf.Price });
                else
                {
                    cco.Quantity++;
                    cco.Total += sf.Price;

                }
            }

            owner.OwnerCart.Total += sf.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<OwnerCartDto>(owner.OwnerCart);
           
            result.SupplierName = sf.Supplier.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }

        public async Task<ApiResponse> RemoveProductFromCart(int userIdentifier, int productId)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            SupplierProduct sp = await _unitOfWork.SupplierProducts.FindAsync(p => p.Id == productId,
                new List<Expression<Func<SupplierProduct, object>>>()
                {
                    p =>p.Supplier.AppUser,
                });

            if (owner.OwnerCart is null)
            {
                return new ApiResponse(200, false, null, "Cart Is Null !");
            }

            OwnerCartProduct product;

            if (owner.OwnerCart.Products.Count() > 0)
                product = owner.OwnerCart.Products.FirstOrDefault(p => p.SupplierProductId == productId);

            else
                return new ApiResponse(200, false, null, "Cart Is Null !");

            if (product is null)
            {
                return new ApiResponse(200, false, null, "Product Not Found !");
            }

            owner.OwnerCart.Products.Remove(product);
            
            owner.OwnerCart.Total -= product.Total;
            
            _unitOfWork.Complete();

            OwnerCartDto result;

            result = _mapper.Map<OwnerCartDto>(owner.OwnerCart);

            result.SupplierName = sp.Supplier.AppUser.Name;

            return new ApiResponse(200, true, result, "Removed Successfully");
        }

        public async Task<ApiResponse> RemoveOfferFromCart(int userIdentifier, int offerId)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            SupplierOffer sf = await _unitOfWork.SupplierOffers.FindAsync(p => p.Id == offerId,
                new List<Expression<Func<SupplierOffer, object>>>()
                {
                    p =>p.Supplier.AppUser,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            if (owner.OwnerCart is null)
            {
                return new ApiResponse(200, false, null, "Cart Is Null !");
            }

            OwnerCartOffer offer ;

            if (owner.OwnerCart.Offers.Count() > 0)
            {
                offer = owner.OwnerCart.Offers.FirstOrDefault(p => p.SupplierOffertId == offerId);
            }
            else
            {
                return new ApiResponse(200, false, null, "Ther Is No Offers!");
            }

            if (offer is null)
            {
                return new ApiResponse(200, false, null, "Offer Not Found !");
            }

            owner.OwnerCart.Offers.Remove(offer);
            
            owner.OwnerCart.Total -= offer.Total;
            
            _unitOfWork.Complete();

            OwnerCartDto result;

            result = _mapper.Map<OwnerCartDto>(owner.OwnerCart);

            result.SupplierName = sf.Supplier.AppUser.Name;

            return new ApiResponse(200, true, result, "Removed Successfully");
        }



        public async Task<ApiResponse> ProductPlus(int userIdentifier, int ProductId)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            SupplierProduct op = await _unitOfWork.SupplierProducts.FindAsync(p => p.Id == ProductId,
                new List<Expression<Func<SupplierProduct, object>>>()
                {
                    p =>p.Supplier.AppUser,
                });

            if (owner.OwnerCart is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }

            OwnerCartDto result;

            if (owner.OwnerCart.OwnerId != op.SupplierId)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place!");

            if (owner.OwnerCart.Products is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }
            else
            {
                OwnerCartProduct ccp = owner.OwnerCart.Products.FirstOrDefault(p => p.SupplierProductId == op.Id);
                if (ccp is null)
                    return new ApiResponse(400, false, null, "The Cart Doesn't Contain this product!");
                else
                {
                    ccp.Quantity++;
                    ccp.Total += op.Price;
                }
            }

            owner.OwnerCart.Total += op.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<OwnerCartDto>(owner.OwnerCart);

            result.SupplierName = op.Supplier.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }
        public async Task<ApiResponse> OfferPlus(int userIdentifier, int offerId)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            SupplierOffer of = await _unitOfWork.SupplierOffers.FindAsync(p => p.Id == offerId,
                new List<Expression<Func<SupplierOffer, object>>>()
                {
                    p =>p.Supplier.AppUser,
                });

            if (owner.OwnerCart is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }

            OwnerCartDto result;

            if (owner.OwnerCart.OwnerId != of.SupplierID)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place!");

            if (owner.OwnerCart.Offers is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }
            else
            {
                OwnerCartOffer ccf = owner.OwnerCart.Offers.FirstOrDefault(p => p.SupplierOffertId == of.Id);
                if (ccf is null)
                    return new ApiResponse(400, false, null, "The Cart Doesn't Contain this Offer!");
                else
                {
                    ccf.Quantity++;
                    ccf.Total += of.Price;
                }
            }

            owner.OwnerCart.Total += of.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<OwnerCartDto>(owner.OwnerCart);

            result.SupplierName = of.Supplier.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }


        public async Task<ApiResponse> ProductMinus(int userIdentifier, int ProductId)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            SupplierProduct op = await _unitOfWork.SupplierProducts.FindAsync(p => p.Id == ProductId,
                new List<Expression<Func<SupplierProduct, object>>>()
                {
                    p =>p.Supplier.AppUser,
                });

            if (owner.OwnerCart is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }

            OwnerCartDto result;

            if (owner.OwnerCart.OwnerId != op.SupplierId)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place!");

            if (owner.OwnerCart.Products is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }
            else
            {
                OwnerCartProduct ccp = owner.OwnerCart.Products.FirstOrDefault(p => p.SupplierProductId == op.Id);
                if (ccp is null)
                    return new ApiResponse(400, false, null, "The Cart Doesn't Contain this product!");
                else
                {
                    ccp.Quantity--;
                    ccp.Total -= op.Price;
                    if (ccp.Quantity == 0)
                    {
                        owner.OwnerCart.Products.Remove(ccp);
                    }
                }
            }

            owner.OwnerCart.Total -= op.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<OwnerCartDto>(owner.OwnerCart);

            result.SupplierName = op.Supplier.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }
        public async Task<ApiResponse> OfferMinus(int userIdentifier, int offerId)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            SupplierOffer of = await _unitOfWork.SupplierOffers.FindAsync(p => p.Id == offerId,
                new List<Expression<Func<SupplierOffer, object>>>()
                {
                    p =>p.Supplier.AppUser,
                });

            if (owner.OwnerCart is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }

            OwnerCartDto result;

            if (owner.OwnerCart.OwnerId != of.SupplierID)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place!");

            if (owner.OwnerCart.Offers is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }
            else
            {
                OwnerCartOffer ccf = owner.OwnerCart.Offers.FirstOrDefault(p => p.SupplierOffertId == of.Id);
                if (ccf is null)
                    return new ApiResponse(400, false, null, "The Cart Doesn't Contain this Offer!");
                else
                {
                    ccf.Quantity--;
                    ccf.Total -= of.Price;
                    if (ccf.Quantity == 0)
                    {
                        owner.OwnerCart.Offers.Remove(ccf);
                    }
                }
            }

            owner.OwnerCart.Total -= of.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<OwnerCartDto>(owner.OwnerCart);

            result.SupplierName = of.Supplier.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }



        public async Task<ApiResponse> GetCreateOrder(int userIdentifier)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            if (owner.OwnerCart is null)
            {
                return new ApiResponse(200, true, null, "Cart Is Empty!");
            }

            int ProductsCount = owner.OwnerCart.Products.Count();
            int OffersCount = owner.OwnerCart.Offers.Count();
            int itemsCount = ProductsCount + OffersCount;

            double total = owner.OwnerCart.Total;

            CreateOrderDto result = new() { ItemsCount = itemsCount, Total = total};

            return new ApiResponse(200, true, result, "Cart Info");
        }

        public async Task<ApiResponse> PostCreateOrder(int userIdentifier)
        {
            Owner owner = await _unitOfWork.Owners
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Owner, object>>>()
                {
                    c => c.OwnerCart.Offers,
                    c => c.OwnerCart.Products,
                });

            if (owner is null)
                return new ApiResponse(401, false);

            if (owner.OwnerCart is null)
            {
                return new ApiResponse(200, true, null, "Cart Is Empty!");
            }

            OwnerOrder order = new OwnerOrder()
            {
                Total = owner.OwnerCart.Total,
                orderDate = DateTime.Now,
                orderStatus = OrderStatus.pending,
                OwnerId = userIdentifier,
                IsDeleted = false,
                SupplierId = owner.OwnerCart.OwnerId,
                PaymentMethod = PaymentMethods.cash
            };

            await _unitOfWork.OwnerOrders.Add(order);
            _unitOfWork.Complete();

            List<OwnerOrderProduct> products = owner.OwnerCart.Products.Select(p => new OwnerOrderProduct { IsDeleted = false, OrderId = order.Id, SupplierProductId = p.Id, Quantity = p.Quantity }).ToList();
            List<OwnerOrderOffer> offers = owner.OwnerCart.Offers.Select(o => new OwnerOrderOffer { IsDeleted = false, OrderId = order.Id, SupplierOffertId = o.Id, Quantity = o.Quantity }).ToList();
            
            order.Products = products;
            order.Offers = offers;
            
            _unitOfWork.Complete();

            return new ApiResponse(200, true, null, "The Order Created Successfully");

        }
    }
}   
