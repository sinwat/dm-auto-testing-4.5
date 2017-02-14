using System;

namespace Dm.Auto.Testing.Core.Browsers.Ajax
{
    public class AjaxRequestFailedException : Exception
    {
        public AjaxRequestFailedException(AjaxRequest request) : base(
            $"Ajax request on url {request.Url} ended with an error {request.HttpStatusCode}")
        {
            
        }
    }
}