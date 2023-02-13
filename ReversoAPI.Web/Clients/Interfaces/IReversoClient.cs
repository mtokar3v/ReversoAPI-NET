﻿using ReversoAPI.Web.Clients.Interfaces;

namespace ReversoAPI.Web.Clients
{
    public interface IReversoClient
    {
        IContextClient Context { get; }
        ISynonimsClient Symonims { get; }
    }
}