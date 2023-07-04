using AutoMapper;
using BusinessObjects.Model;
using DataAccess.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ItemInventoryRepositories
{
    public class ItemInventoryRepositories : GenericRepository<ItemIventory> , IItemInventoryRepositories
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public ItemInventoryRepositories(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

    }
}
