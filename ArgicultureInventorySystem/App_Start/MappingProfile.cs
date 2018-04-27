using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgicultureInventorySystem.Dtos;
using ArgicultureInventorySystem.Models;
using AutoMapper;

namespace ArgicultureInventorySystem.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Stock, StockDto>();
            Mapper.CreateMap<StockDto, Stock>();
        }
    }
}