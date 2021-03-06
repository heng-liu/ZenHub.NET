using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ZenHub.Pipeline
{
    internal class ThrowOnErrorStatusPolicy : HttpPipelinePolicy
    {
        public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            ProcessNext(message, pipeline);
            if (message.ResponseClassifier.IsErrorResponse(message))
            {
                throw new RequestFailedException(message.Response.Status, message.ToString());
            }
        }

        public override async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            await ProcessNextAsync(message, pipeline);
            if (message.ResponseClassifier.IsErrorResponse(message))
            {
                throw new RequestFailedException(message.Response.Status, message.ToString());
            }
        }
    }
}
