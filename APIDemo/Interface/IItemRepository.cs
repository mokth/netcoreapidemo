using APIDemo.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIDemo.Repository.Interface
{
    public interface IItemRepository
    {
        public Task<RespResult<ItemMaster>> AddItemMaster(ItemMaster item);
        public Task<RespResult<bool>> BulkInsertItemMaster(List<ItemMaster> items);
        public Task<RespResult<ItemMaster>> DeleteItemMaster(ItemMaster item);
        public Task<RespResult<ItemMaster>> DeleteItemMasterByCode(string code);
        public Task<ItemMaster> GetItemByCode(string code);
        public Task<IEnumerable<ItemMaster>> GetItemMasters();
        public Task<IEnumerable<ItemMasterView>> GetItemMasterView();
        public Task<RespResult<ItemMaster>> UpdateItemMaster(ItemMaster item);
    }
}