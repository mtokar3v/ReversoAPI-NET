﻿using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace ReversoAPI.Web
{
    public class APIConnector : IAPIConnector
    {
        private readonly IHttpClient _httpClient;

        private const int RetryAttemptCount = 4;
        private static readonly HttpStatusCode[] _httpStatusCodesWorthRetrying =
        {
           HttpStatusCode.RequestTimeout,
           HttpStatusCode.InternalServerError,
           HttpStatusCode.BadGateway,
           HttpStatusCode.ServiceUnavailable,
           HttpStatusCode.GatewayTimeout
        };

        public APIConnector(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponse> GetAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            var response = await Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponse>(r => _httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                .WaitAndRetryAsync(RetryAttemptCount, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)))
                .ExecuteAsync(() => _httpClient.GetAsync(uri, cancellationToken))
                .ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponse> PostAsync(Uri uri, object payload, CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (payload == null) throw new ArgumentNullException(nameof(payload));

            var response = await Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponse>(r => _httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                .WaitAndRetryAsync(RetryAttemptCount, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)))
                .ExecuteAsync(() => _httpClient.PostAsync(uri, payload, cancellationToken))
                .ConfigureAwait(false);

            return response;
        }
    }
}
