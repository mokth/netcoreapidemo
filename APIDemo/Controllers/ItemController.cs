using APIDemo.Model;
using APIDemo.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemHelper _helper;
        private ILogger<ItemController> _logger;

        public ItemController(IItemHelper helper, ILogger<ItemController> logger)
        {
            _helper = helper;
            _logger = logger ;
        }

        [HttpGet, Route("items")]        
        public async Task<IActionResult> GetAllItems()
        {  
            var items = await _helper.GetItemMasters();
            return Json(new
            {
                ok = (items.Count()>0)? "yes":"no",
                data = items,
                error = (items.Count() > 0) ? "" : "no item found..",
            });
        }

        [HttpGet, Route("viewitems")]
        public async Task<IActionResult> GetSqlViewItems()
        {
            var items = await _helper.GetItemMasterView();
            return Json(new
            {
                ok = (items.Count() > 0) ? "yes" : "no",
                data = items,
                error = (items.Count() > 0) ? "" : "no item found..",
            });
        }

        [HttpGet, Route("{code}")]
        public async Task<IActionResult> GetItemByCode(string code)
        {
            var item = await _helper.GetItemByCode(code);
            return Json(new
            {
                ok = (item!=null) ? "yes" : "no",
                data = item,
                error = (item!=null) ? "" : "no item found..",
            });
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> AddNewItem([FromBody] ItemMaster item)
        {
            var result = await _helper.AddItemMaster(item);

            return Json(new
            {
                ok = result.IsSuccess ? "yes" : "no",
                data = result.Result,
                error = result.ErrorMsg
            }) ;
        }

        [HttpPost, Route("bulkadd")]
        public async Task<IActionResult> AddBulkItem([FromBody] List<ItemMaster> items)
        {
            var result = await _helper.BulkInsertItemMaster(items);

            return Json(new
            {
                ok = result.IsSuccess ? "yes" : "no",
                data = result.Result,
                error = result.ErrorMsg
            });
        }

        [HttpPut, Route("update")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemMaster item)
        {
            var result = await _helper.UpdateItemMaster(item);

            return Json(new
            {
                ok = result.IsSuccess ? "yes" : "no",
                data = result.Result,
                error = result.ErrorMsg
            });
        }

        [HttpDelete, Route("delete/{code}")]
        public async Task<IActionResult> DeleteItem(string code)
        {
            var result = await _helper.DeleteItemMasterByCode(code);

            return Json(new
            {
                ok = result.IsSuccess ? "yes" : "no",
                data = result.Result,
                error = result.ErrorMsg
            });
        }

    }
}
