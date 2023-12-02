using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRestClient;

public struct RequestConfig<TContext>
{
    public string Endpoint { get; set; }
    public List<KeyValuePair<string, dynamic>>? QueryParams { get; set; }
    public TContext? Context { get; set; }
    public Func<TContext, HttpRequestMessage> MessageBuilder { get; set; }
}
