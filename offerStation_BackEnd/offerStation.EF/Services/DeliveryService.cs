using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeliveryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddDelivery(DeliveryDto deliveryDto)
        {
            if (deliveryDto is not null)
            {
                Delivery delivery = new Delivery();
                delivery = _mapper.Map<Delivery>(deliveryDto);

                _unitOfWork.Deliveries.Add(delivery);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> EditDelivery(int id, DeliveryDto deliveryDto)
        {
            Delivery delivery = await _unitOfWork.Deliveries.GetByIdAsync(id);

            if (delivery is not null)
            {
                delivery.Name = deliveryDto.Name;
                delivery.Phone = deliveryDto.Phone;

                _unitOfWork.Deliveries.Update(delivery);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteDelivery(int id)
        {
            Delivery delivery = await _unitOfWork.Deliveries.GetByIdAsync(id);

            if (delivery is not null)
            {
                _unitOfWork.Deliveries.Delete(delivery);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
    }
}
