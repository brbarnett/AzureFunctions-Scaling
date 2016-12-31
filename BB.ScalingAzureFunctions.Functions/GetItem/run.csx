using System;
using System.Net;
using System.Runtime.CompilerServices;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    // simulate database query
    await Task.Run(() => System.Threading.Thread.Sleep(1000));

    // return new item
    return req.CreateResponse(HttpStatusCode.OK, new { Id = Guid.NewGuid() });
}