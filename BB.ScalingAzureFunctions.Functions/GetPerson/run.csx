#r "System.Runtime.Serialization"

using System;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Runtime.Serialization;

public static HttpResponseMessage Run(RequestData requestData, HttpRequestMessage req, Person person, TraceWriter log)
{
    // return new item
    return req.CreateResponse(HttpStatusCode.OK, person);
}

[DataContract(Name = "RequestData", Namespace = "http://functions")]
public class RequestData
{
    [DataMember]
    public string Id { get; set; }
}

public class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime LastUpdated { get; set; }
}