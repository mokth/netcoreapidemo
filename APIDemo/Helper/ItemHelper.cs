using APIDemo.Model;
using APIDemo.Repository.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Helper
{
    public class ItemHelper: IItemHelper
    {
        protected IItemRepository _repo;
        protected ILogger<ItemHelper> _logger;


        //her inject all the services in
        public ItemHelper(IItemRepository repo,
                          ILogger<ItemHelper> logger)
        {
            _repo = repo;
            _logger = logger;
        }


        #region ItemMaster

        public async Task<RespResult<bool>> BulkInsertItemMaster(List<ItemMaster> items)
        {
            return await _repo.BulkInsertItemMaster(items);
        }
        public async Task<ItemMaster> GetItemByCode(string code)
        {
            return await _repo.GetItemByCode(code);
        }

        public async Task<IEnumerable<ItemMaster>> GetItemMasters()
        {
            return await _repo.GetItemMasters();
        }

        public async Task<IEnumerable<ItemMasterView>> GetItemMasterView()
        {
            return await _repo.GetItemMasterView();
        }

        public async Task<RespResult<ItemMaster>> AddItemMaster(ItemMaster item)
        {
            item.CreatedOn = DateTime.Now;
            return await _repo.AddItemMaster(item);
        }

        public async Task<RespResult<ItemMaster>> UpdateItemMaster(ItemMaster item)
        {
            var found = await _repo.GetItemByCode(item.Code);
            if (found == null)
            {
                RespResult<ItemMaster> result = new RespResult<ItemMaster>();
                result.IsSuccess = false;
                result.ErrorMsg = "Item code not found in database " + item.Code;
                _logger.LogError("result.ErrorMsg");
                return result;
            }
            return await _repo.UpdateItemMaster(item);
        }

        public async Task<RespResult<ItemMaster>> DeleteItemMaster(ItemMaster item)
        {
            return await _repo.DeleteItemMaster(item);
        }

        public async Task<RespResult<ItemMaster>> DeleteItemMasterByCode(string code)
        {
            return await _repo.DeleteItemMasterByCode(code);
        }

        #endregion ItemMaster
    }
}
