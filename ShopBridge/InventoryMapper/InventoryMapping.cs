using AutoMapper;
using ShopBridge.Models;
using ShopBridge.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.InventoryMapper
{
    public class InventoryMapping : Profile
    {
        public InventoryMapping()
        {
            CreateMap<Inventory, InventoryDto>().ReverseMap();
            CreateMap<InventoryDto, Inventory>().ReverseMap();
        }
    }
}
