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
    public class CustomerCartService : ICustomerCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CustomerCartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetCartDetails(int userIdentifier)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            if (customer is null)
                return new ApiResponse(401, false);

            if (customer.CustomerCart is null)
            {
                return new ApiResponse(200, true, null, "Cart Is Empty!");
            }

            CustomerCartDto result;
            string ownername = "";
            int productsCount = customer.CustomerCart.Products.Count();
            int offersCount = customer.CustomerCart.Offers.Count();

            if(productsCount > 0 || offersCount > 0)
            {
                if (productsCount > 0)
                {
                    int currentId = customer.CustomerCart.Products[0].Id;

                    OwnerProduct op = await _unitOfWork.OwnerProducts.FindAsync(p => p.Id == currentId,
                    new List<Expression<Func<OwnerProduct, object>>>()
                    {
                    p => p.Owner.AppUser,
                    });
                    ownername = op.Owner.AppUser.Name;
                }
                else
                {
                    int currentId = customer.CustomerCart.Offers[0].Id;

                    OwnerOffer of = await _unitOfWork.OwnerOffers.FindAsync(o => o.Id == currentId,
                    new List<Expression<Func<OwnerOffer, object>>>()
                    {
                    o => o.Owner.AppUser,
                    });
                    ownername = of.Owner.AppUser.Name;
                }
            }
            else
            {
                return new ApiResponse(200, true, null, "Cart Is Empty!");
            }

            result = _mapper.Map<CustomerCartDto>(customer.CustomerCart);

            result.OwnerName = ownername;

            return new ApiResponse(200, true, result, "Cart Info");
        }

        public async Task<ApiResponse> AddProductToCart(int userIdentifier, ProductDetailsDto Product)
        {
            Customer customer = await _unitOfWork.Customers 
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted, 
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            if(customer is null)
                return new ApiResponse(401, false);

            OwnerProduct op = await _unitOfWork.OwnerProducts.FindAsync(p => p.Id == Product.Id,
                new List<Expression<Func<OwnerProduct, object>>>()
                {
                    p =>p.Owner.AppUser,
                });

            if(customer.CustomerCart is null)
            {
                CustomerCart cart = new() { CustomerId = userIdentifier , OwnerId = op.OwnerId };

                _unitOfWork.CustomerCarts.Add(cart);

                customer.CustomerCart = cart;

                _unitOfWork.Complete();
            }

            CustomerCartDto result;

            if (customer.CustomerCart.OwnerId != op.OwnerId)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place !");

            if (customer.CustomerCart.Products is null)
            {
                CustomerCartProduct customerCartProduct = new ()
                {
                    OwnerProductId = op.Id,
                    CartId = customer.CustomerCart.Id,
                    Quantity = 1,
                    Price = op.Price,
                    Total = op.Price
                };

                _unitOfWork.CustomerCartProducts.Add(customerCartProduct);
                _unitOfWork.Complete();

                List<CustomerCartProduct> customerCartProductsList = new List<CustomerCartProduct>();
                customerCartProductsList.Add(customerCartProduct);

                customer.CustomerCart.Products = customerCartProductsList;
            }
            else
            {
                CustomerCartProduct ccp = customer.CustomerCart.Products.FirstOrDefault(p => p.OwnerProductId == op.Id);
                if (ccp is null)
                    customer.CustomerCart.Products.Add(new CustomerCartProduct { OwnerProductId = op.Id, OwnerProduct = op, Quantity = 1, Price = op.Price,Total = op.Price });
                else
                {
                    ccp.Quantity++;
                    ccp.Total += op.Price;
                }
            }
            
            customer.CustomerCart.Total += op.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<CustomerCartDto>(customer.CustomerCart);
            
            result.OwnerName = op.Owner.AppUser.Name;


            return new ApiResponse(200, true, result, "Added Successfully");
        }

        public async Task<ApiResponse> AddOfferToCart(int userIdentifier, ProductDetailsDto Offer)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            if (customer is null)
                return new ApiResponse(401, false);

            OwnerOffer of = await _unitOfWork.OwnerOffers.FindAsync(o => o.Id == Offer.Id,
                new List<Expression<Func<OwnerOffer, object>>>()
                {
                    o =>o.Owner.AppUser,
                });

            if (customer.CustomerCart is null)
            {
                CustomerCart cart = new() { CustomerId = userIdentifier, OwnerId = of.OwnerId };

                _unitOfWork.CustomerCarts.Add(cart);

                customer.CustomerCart = cart;

                _unitOfWork.Complete();
            }
            CustomerCartDto result;

            if (customer.CustomerCart.OwnerId != of.OwnerId)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place !");

            if (customer.CustomerCart.Offers is null)
            {
                CustomerCartOffer customerCartOffer = new()
                {
                    OwnerOffertId = of.Id,
                    CartId = customer.CustomerCart.Id,
                    Quantity = 1,
                    Price = of.Price,
                    Total = of.Price
                };
                _unitOfWork.CustomerCartOffers.Add(customerCartOffer);
                _unitOfWork.Complete();

                List<CustomerCartOffer> customerCartProductsList = new List<CustomerCartOffer>();
                customerCartProductsList.Add(customerCartOffer);

                customer.CustomerCart.Offers = customerCartProductsList;

            }
            else
            {
                CustomerCartOffer cco = customer.CustomerCart.Offers.FirstOrDefault(o => o.OwnerOffertId == of.Id);
                if (cco is null)
                    customer.CustomerCart.Offers.Add(new CustomerCartOffer { OwnerOffertId = of.Id, OwnerOffer = of, Quantity = 1,Price = of.Price,Total = of.Price });
                else
                {
                    cco.Quantity++;
                    cco.Total += of.Price;

                }
            }
            
            customer.CustomerCart.Total += of.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<CustomerCartDto>(customer.CustomerCart);
           
            result.OwnerName = of.Owner.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }

        public async Task<ApiResponse> RemoveProductFromCart(int userIdentifier, int productId)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            OwnerProduct op = await _unitOfWork.OwnerProducts.FindAsync(p => p.Id == productId,
                new List<Expression<Func<OwnerProduct, object>>>()
                {
                    p =>p.Owner.AppUser,
                });

            if (customer is null)
                return new ApiResponse(401, false);


            if (customer.CustomerCart is null)
            {
                return new ApiResponse(200, false, null, "Cart Is Null !");
            }
            CustomerCartProduct product;
            
            if (customer.CustomerCart.Products.Count() > 0)
                product = customer.CustomerCart.Products.FirstOrDefault(p => p.OwnerProductId == productId);
            
            else
                return new ApiResponse(200, false, null, "Cart Is Null !");


            if (product is null)
            {
                return new ApiResponse(200, false, null, "Product Not Found !");
            }

            customer.CustomerCart.Products.Remove(product);
            
            customer.CustomerCart.Total -= product.Total;

            _unitOfWork.Complete();

            CustomerCartDto result;

            result = _mapper.Map<CustomerCartDto>(customer.CustomerCart);

            result.OwnerName = op.Owner.AppUser.Name;

            return new ApiResponse(200, true, result, "Removed Successfully");
        }

        public async Task<ApiResponse> RemoveOfferFromCart(int userIdentifier, int offerId)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            OwnerOffer op = await _unitOfWork.OwnerOffers.FindAsync(p => p.Id == offerId,
                new List<Expression<Func<OwnerOffer, object>>>()
                {
                    p =>p.Owner.AppUser,
                });

            if (customer is null)
                return new ApiResponse(401, false);

            if (customer.CustomerCart is null)
            {
                return new ApiResponse(200, false, null, "Cart Is Null !");
            }

            CustomerCartOffer offer;

            if(customer.CustomerCart.Offers.Count() > 0)
            {
                offer = customer.CustomerCart.Offers.FirstOrDefault(p => p.OwnerOffertId == offerId);
            }
            else
            {
                return new ApiResponse(200, false, null, "Ther Is No Offers!");
            }

            if (offer is null)
            {
                return new ApiResponse(200, false, null, "Offer Not Found !");
            }
            
            customer.CustomerCart.Offers.Remove(offer);
            
            customer.CustomerCart.Total -= offer.Total;
            
            _unitOfWork.Complete();

            CustomerCartDto result;

            result = _mapper.Map<CustomerCartDto>(customer.CustomerCart);

            result.OwnerName = op.Owner.AppUser.Name;

            return new ApiResponse(200, true, result, "Removed Successfully");
        }


        public async Task<ApiResponse> ProductPlus(int userIdentifier, int ProductId)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            if (customer is null)
                return new ApiResponse(401, false);

            OwnerProduct op = await _unitOfWork.OwnerProducts.FindAsync(p => p.Id == ProductId,
                new List<Expression<Func<OwnerProduct, object>>>()
                {
                    p =>p.Owner.AppUser,
                });

            if (customer.CustomerCart is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }

            CustomerCartDto result;

            if (customer.CustomerCart.OwnerId != op.OwnerId)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place!");

            if (customer.CustomerCart.Products is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }
            else
            {
                CustomerCartProduct ccp = customer.CustomerCart.Products.FirstOrDefault(p => p.OwnerProductId == op.Id);
                if (ccp is null)
                    return new ApiResponse(400, false, null, "The Cart Doesn't Contain this product!");
                else
                {
                    ccp.Quantity++;
                    ccp.Total += op.Price;
                }
            }

            customer.CustomerCart.Total += op.Price;

            _unitOfWork.Complete();
            
            result = _mapper.Map<CustomerCartDto>(customer.CustomerCart);

            result.OwnerName = op.Owner.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }
        public async Task<ApiResponse> OfferPlus(int userIdentifier, int offerId)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            if (customer is null)
                return new ApiResponse(401, false);

            OwnerOffer of = await _unitOfWork.OwnerOffers.FindAsync(p => p.Id == offerId,
                new List<Expression<Func<OwnerOffer, object>>>()
                {
                    p =>p.Owner.AppUser,
                });

            if (customer.CustomerCart is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Offers PLZ!");
            }

            CustomerCartDto result;

            if (customer.CustomerCart.OwnerId != of.OwnerId)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place!");

            if (customer.CustomerCart.Offers is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Offers PLZ!");
            }
            else
            {
                CustomerCartOffer ccf = customer.CustomerCart.Offers.FirstOrDefault(p => p.OwnerOffertId == of.Id);
                if (ccf is null)
                    return new ApiResponse(400, false, null, "The Cart Doesn't Contain this Offer!");
                else
                {
                    ccf.Quantity++;
                    ccf.Total += of.Price;
                }
            }

            customer.CustomerCart.Total += of.Price;
            
            _unitOfWork.Complete();

            result = _mapper.Map<CustomerCartDto>(customer.CustomerCart);

            result.OwnerName = of.Owner.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }


        public async Task<ApiResponse> ProductMinus(int userIdentifier, int ProductId)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            if (customer is null)
                return new ApiResponse(401, false);

            OwnerProduct op = await _unitOfWork.OwnerProducts.FindAsync(p => p.Id == ProductId,
                new List<Expression<Func<OwnerProduct, object>>>()
                {
                    p =>p.Owner.AppUser,
                });

            if (customer.CustomerCart is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }

            CustomerCartDto result;

            if (customer.CustomerCart.OwnerId != op.OwnerId)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place!");

            if (customer.CustomerCart.Products is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Products PLZ!");
            }
            else
            {
                CustomerCartProduct ccp = customer.CustomerCart.Products.FirstOrDefault(p => p.OwnerProductId == op.Id);
                if (ccp is null)
                    return new ApiResponse(400, false, null, "The Cart Doesn't Contain this product!");
                else
                {
                    ccp.Quantity--;
                    ccp.Total -= op.Price;

                    if (ccp.Quantity == 0)
                    {
                        customer.CustomerCart.Products.Remove(ccp);
                    }
                }
            }

            customer.CustomerCart.Total -= op.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<CustomerCartDto>(customer.CustomerCart);

            result.OwnerName = op.Owner.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }
        public async Task<ApiResponse> OfferMinus(int userIdentifier, int offerId)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            if (customer is null)
                return new ApiResponse(401, false);

            OwnerOffer of = await _unitOfWork.OwnerOffers.FindAsync(p => p.Id == offerId,
                new List<Expression<Func<OwnerOffer, object>>>()
                {
                    p =>p.Owner.AppUser,
                });

            if (customer.CustomerCart is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Offers PLZ!");
            }

            CustomerCartDto result;

            if (customer.CustomerCart.OwnerId != of.OwnerId)
                return new ApiResponse(200, false, null, "You Can Only Buy From One Place!");

            if (customer.CustomerCart.Offers is null)
            {
                return new ApiResponse(400, false, null, "the cart is Empty Add some Offers PLZ!");
            }
            else
            {
                CustomerCartOffer ccf = customer.CustomerCart.Offers.FirstOrDefault(p => p.OwnerOffertId == of.Id);
                if (ccf is null)
                    return new ApiResponse(400, false, null, "The Cart Doesn't Contain this Offer!");
                else
                {
                    ccf.Quantity--;
                    ccf.Total -= of.Price;

                    if(ccf.Quantity == 0)
                    {
                        customer.CustomerCart.Offers.Remove(ccf);
                    }
                }
            }
            
            customer.CustomerCart.Total -= of.Price;

            _unitOfWork.Complete();

            result = _mapper.Map<CustomerCartDto>(customer.CustomerCart);

            result.OwnerName = of.Owner.AppUser.Name;

            return new ApiResponse(200, true, result, "Added Successfully");
        }

        public async Task<ApiResponse> GetCreateOrder(int userIdentifier)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            if (customer is null)
                return new ApiResponse(401, false);

            if (customer.CustomerCart is null)
            {
                return new ApiResponse(200, true, null, "Cart Is Empty!");
            }

            int ProductsCount = customer.CustomerCart.Products.Count();
            int OffersCount = customer.CustomerCart.Offers.Count();
            int itemsCount = ProductsCount + OffersCount;

            double total = customer.CustomerCart.Total;

            CreateOrderDto result = new() { ItemsCount = itemsCount, Total = total};

            return new ApiResponse(200, true, result, "Cart Info");
        }

        public async Task<ApiResponse> PostCreateOrder(int userIdentifier)
        {
            Customer customer = await _unitOfWork.Customers
                .FindAsync(c => c.Id == userIdentifier && !c.IsDeleted,
                new List<Expression<Func<Customer, object>>>()
                {
                    c => c.CustomerCart.Offers,
                    c => c.CustomerCart.Products,
                });

            if (customer is null)
                return new ApiResponse(401, false);

            if (customer.CustomerCart is null)
            {
                return new ApiResponse(200, true, null, "Cart Is Empty!");
            }

            CustomerOrder order = new CustomerOrder()
            {
                Total = customer.CustomerCart.Total,
                orderDate = DateTime.Now,
                orderStatus = OrderStatus.pending,
                CustomerId = userIdentifier,
                IsDeleted = false,
                OwnerId = customer.CustomerCart.OwnerId,
                PaymentMethod = PaymentMethods.cash
            };

            await _unitOfWork.CustomerOrders.Add(order);
            _unitOfWork.Complete();

            List<CustomerOrderProduct> products = customer.CustomerCart.Products.Select(p => new CustomerOrderProduct { IsDeleted = false, OrderId = order.Id, OwnerProductId = p.Id, Quantity = p.Quantity }).ToList();
            List<CustomerOrderOffer> offers = customer.CustomerCart.Offers.Select(o => new CustomerOrderOffer { IsDeleted = false, OrderId = order.Id, OwnerOffertId = o.Id, Quantity = o.Quantity }).ToList();
            
            order.Products = products;
            order.Offers = offers;
            
            _unitOfWork.Complete();

            customer.CustomerCart = null;
            _unitOfWork.Complete();

            return new ApiResponse(200, true, null, "The Order Created Successfully");

        }
    }
}   
