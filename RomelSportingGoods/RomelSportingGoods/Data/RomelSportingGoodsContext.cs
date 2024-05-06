using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RomelSportingGoods.Models;

namespace RomelSportingGoods.Data
{
    public class RomelSportingGoodsContext : DbContext
    {
        public RomelSportingGoodsContext (DbContextOptions<RomelSportingGoodsContext> options)
            : base(options)
        {
        }

        public DbSet<RomelSportingGoods.Models.Product> Product { get; set; } = default!;
        public DbSet<RomelSportingGoods.Models.UserRomelSportsGoods> UserRomelSportsGoods { get; set; } = default!;

    }
}
