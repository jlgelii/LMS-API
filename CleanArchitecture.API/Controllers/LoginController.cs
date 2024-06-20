using CleanArchitecture.Application.UserAccounts.Queries.GetUserByLogin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        /// <summary>
        /// Get user by login
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<GetUserByLoginDto>> GetUserByLogin(GetUserByLoginQuery query)
        {
            var response = await _mediator.Send(query);
            if (response.Error)
                return BadRequest(response.ModelStateError);

            return Ok(response.Data);
        }
    }
}
