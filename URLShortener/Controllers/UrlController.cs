using AutoMapper;
using URLShortener.BLL.DTO;
using URLShortener.BLL.MediatR.Url.GetAll;
using URLShortener.Controllers.BaseController;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using URLShortener.BLL.Services.Interfaces;
using URLShortener.BLL.MediatR.Url.Create;
using URLShortener.BLL.MediatR.Questionnaire.Get;
using URLShortener.BLL.MediatR.Url.Delete;
using URLShortener.BLL.MediatR.Url.GetById;

namespace URLShortener.Controllers
{
    public class UrlController : BaseApiController
    {

        [HttpPost]
        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> ShortenUrl([FromBody] LongUrlDTO request)
        {
            return HandleResult(await Mediator.Send(new CreateUrlCommand(request)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetUrlByIdQuery(id)));
        }

        [HttpGet("{shortenedUrl}")]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortenedUrl)
        {
            var originalUrl = HandleResult(await Mediator.Send(new GetUrlByShortUrlQuery(shortenedUrl)));

            if (originalUrl is OkObjectResult redirectResult)
            {
                return Redirect(redirectResult.Value.ToString());
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllUrlsQuery()));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteUrlByIdCommand(id)));
        }
    }
}
