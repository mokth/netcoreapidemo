using APIDemo.Context;
using APIDemo.Model;
using APIDemo.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Repository
{
    public class ItemRepository : IItemRepository
    {
        protected DatabaseContext _context;
        protected ILogger<ItemRepository> _logger;


        //her inject all the services in
        public ItemRepository(DatabaseContext context,
                             ILogger<ItemRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        #region ItemMaster
        public async Task<ItemMaster> GetItemByCode(string code)
        {

            var item = await _context.ItemMasters
                              .Where(x => x.Code == code)
                                .AsNoTracking()  // for best performance, something like read only mode
                                .FirstOrDefaultAsync();
            if (item == null)

            {
                _logger.LogDebug(string.Format("Item code {0} not found.", code));
            }

            return item;
        }

        public async Task<IEnumerable<ItemMaster>> GetItemMasters()
        {
            return await _context.ItemMasters
                        .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ItemMasterView>> GetItemMasterView()
        {
            return await _context.ItemMasterSqlView
                        .AsNoTracking().ToListAsync();
        }

        public async Task<RespResult<ItemMaster>> AddItemMaster(ItemMaster item)
        {
            RespResult<ItemMaster> result = new RespResult<ItemMaster>();
            result.IsSuccess = false;

            try
            {
                _context.ItemMasters.Add(item);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Result = item;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                result.ErrorMsg = "Error adding ItemMaster Code " + item.Code;
            }

            return result;
        }

        public async Task<RespResult<bool>> BulkInsertItemMaster(List<ItemMaster> items)
        {
            RespResult<bool> result = new RespResult<bool>();
            result.IsSuccess = false;

            try
            {
                _context.BulkInsert<ItemMaster>(items);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Result = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                result.ErrorMsg = "Error bulk adding Items Master " ;
            }

            return result;
        }

        public async Task<RespResult<ItemMaster>> UpdateItemMaster(ItemMaster item)
        {
            RespResult<ItemMaster> result = new RespResult<ItemMaster>();
            result.IsSuccess = false;
            try
            {
                var found = await _context.ItemMasters.Where(x => x.Code == item.Code).FirstOrDefaultAsync();
                if (found == null)
                {
                    _logger.LogError("ItemMaster code " + item.Code + " not found!");
                    result.ErrorMsg = "ItemMaster " + item.Code + " not found!";
                    return result;
                }
                found.Name = item.Name;
                found.Active = item.Active;
                found.CreatedOn = DateTime.Now;

                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Result = item;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                result.ErrorMsg = "Error updating ItemMaster " + item.Code;
            }

            return result;
        }

        public async Task<RespResult<ItemMaster>> DeleteItemMaster(ItemMaster item)
        {
            RespResult<ItemMaster> result = new RespResult<ItemMaster>();
            result.IsSuccess = false;
            try
            {
                var found = await _context.ItemMasters.Where(x => x.Code == item.Code).FirstOrDefaultAsync();
                if (found == null)
                {
                    _logger.LogError("ItemMaster Code " + item.Code + " not found!");
                    result.ErrorMsg = "ItemMaster Code " + item.Code + " not found!";
                    return result;
                }
                _context.Remove(found);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Result = item;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                result.ErrorMsg = "Error deleting ItemMaster Code " + item.Code;
            }

            return result;
        }

        public async Task<RespResult<ItemMaster>> DeleteItemMasterByCode(string code)
        {
            RespResult<ItemMaster> result = new RespResult<ItemMaster>();
            result.IsSuccess = false;
            try
            {
                var found = await _context.ItemMasters.Where(x => x.Code == code).FirstOrDefaultAsync();
                if (found == null)
                {
                    _logger.LogError("ItemMaster Code " + code + " not found!");
                    result.ErrorMsg = "ItemMaster Code " + code + " not found!";
                    return result;
                }
                _context.Remove(found);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Result = found;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                result.ErrorMsg = "Error deleting ItemMaster Code " + code;
            }

            return result;
        }

        #endregion ItemMaster
    }
}
