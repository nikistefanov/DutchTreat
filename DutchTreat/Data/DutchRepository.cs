﻿using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            this._ctx = ctx;
            this._logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return this._ctx.Products
                            .OrderBy(p => p.Title)
                            .ToList();

            }
            catch (Exception ex)
            {
                this._logger.LogError($"Failed to get all products: {ex}");

                return null;
            }
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            try
            {
                return this._ctx.Products
                           .Where(p => p.Category == category)
                           .ToList();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Failed to get product by category {category}: {ex}");

                return null;
            }
        }

        public bool SaveAll()
        {
            return this._ctx.SaveChanges() > 0;
        }
    }
}
