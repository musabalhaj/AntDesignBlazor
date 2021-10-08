using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class StatusCodeService
    {
        private readonly NavigationManager navigationManager;

        public StatusCodeService(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
        public async Task HandleStatusCode(HttpResponseMessage responseMessage)
        {
            var statusCode = responseMessage.StatusCode;
            var content = await responseMessage.Content.ReadAsStringAsync();
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    navigationManager.NavigateTo($"/servererror/{content}");
                    break;
                case HttpStatusCode.NotFound:
                    navigationManager.NavigateTo($"/error404/{content}");
                    break;
                case HttpStatusCode.InternalServerError:
                    navigationManager.NavigateTo($"/error500/{content}");
                    break;
                default:
                    navigationManager.NavigateTo("/error400");
                    break;
            }
        }
    }
}
