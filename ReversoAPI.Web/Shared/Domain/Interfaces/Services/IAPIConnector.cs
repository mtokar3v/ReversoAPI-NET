﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web
{
    public interface IAPIConnector
    {
        Task<HttpResponse> GetAsync(Uri uri, CancellationToken cancellationToken = default);
        Task<HttpResponse> PostAsync(Uri uri, object payload, CancellationToken cancellationToken = default);
    }
}