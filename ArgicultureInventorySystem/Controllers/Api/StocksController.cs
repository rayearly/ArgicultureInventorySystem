using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using ArgicultureInventorySystem.Dtos;
using ArgicultureInventorySystem.Models;
using AutoMapper;

namespace ArgicultureInventorySystem.Controllers.Api
{
    public class StocksController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public StocksController()
        {
            _context = new ApplicationDbContext();
            _context.Configuration.ProxyCreationEnabled = false;
        }

        // GET /api/stocks
        public IEnumerable<StockDto> GetStocks()
        {
            return _context.Stocks.ToList().Select(Mapper.Map<Stock, StockDto>);
        }

        // GET /api/stocks/1
        public IHttpActionResult GetStock(int id)
        {
            var stock = _context.Stocks.SingleOrDefault(s => s.Id == id);

            if (stock == null)
                return NotFound();

            return Ok(Mapper.Map<Stock, StockDto>(stock));
        }

        // POST /api/stocks
        [HttpPost]
        public IHttpActionResult CreateStock(StockDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var stock = Mapper.Map<StockDto, Stock>(stockDto);

            _context.Stocks.Add(stock);
            _context.SaveChanges();

            stockDto.Id = stock.Id;

            return Created(new Uri(Request.RequestUri + "/" + stock.Id), stockDto);
        }

        // PUT /api/stocks/1
        [HttpPut]
        public void UpdateStock(int id, Stock stockDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var stockInDb = _context.Stocks.SingleOrDefault(s => s.Id == id);

            if (stockInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // TODO: cannot map here???
            Mapper.Map(stockDto, stockInDb);

            _context.SaveChanges();
        }

        // DELETE /api/stocks/1
        [HttpDelete]
        public IHttpActionResult DeleteStock(int id)
        {
            var stockInDb = _context.Stocks.SingleOrDefault(s => s.Id == id);

            if (stockInDb == null)
                return NotFound();

            _context.Stocks.Remove(stockInDb);
            _context.SaveChanges();

            return Ok();

        }
    }
}

