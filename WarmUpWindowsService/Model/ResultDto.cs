﻿#region

using System.Net;

#endregion

namespace WarmUpWindowsService
{
    public class ResultDto<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }

        public bool IsSuccessCode
        {
            get
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.Accepted:
                    case HttpStatusCode.NonAuthoritativeInformation:
                    case HttpStatusCode.NoContent:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}