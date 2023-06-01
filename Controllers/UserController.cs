using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZwajApp.API.Service.Interfaces;
using ZwajApp.API.Service.Services;

namespace ZwajApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
                
        }


        //[HttpPost]
        //public async Task<IActionResult> Add([FromBody] userAddVM vm)
        //{
        //    //if (!string.IsNullOrEmpty(vm.code))
        //    //{
        //    //    if(await _userService.CheckCodeExist(vm.code))
        //    //    {
        //    //        return BadRequest("Code is aready exist");
        //    //    }
        //    //}
        //    var res = await _userService.Add(userAddVM);
        //    if(res< =0)
        //    {
        //        return BadRequest("ItemNotfound");
        //    }
        //    return Ok(res);

        //}
    }
}
