using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRestClient;

public class RequestConfig<TContext> : RequestConfig
{
    public string Endpoint { get; set; }
    public List<KeyValuePair<string, dynamic>>? QueryParams { get; set; }
    public TContext? Context { get; set; }
    new public Func<TContext, HttpRequestMessage> MessageBuilder { get; set; }

    public override HttpRequestMessage BuildMessage()
    {
        var message = MessageBuilder(Context);

        return message;
    }
}

public class RequestConfig
{
    public string Endpoint { get; set; }
    public List<KeyValuePair<string, dynamic>>? QueryParams { get; set; }
    public Func<HttpRequestMessage> MessageBuilder { get; set; }

    public virtual HttpRequestMessage BuildMessage()
    {
        var message = MessageBuilder();

        return message;
    }
}
