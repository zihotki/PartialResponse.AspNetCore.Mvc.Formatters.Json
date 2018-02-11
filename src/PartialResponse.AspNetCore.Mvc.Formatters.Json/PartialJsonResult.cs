// Copyright (c) Arjen Post. See License.txt and Notice.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PartialResponse.AspNetCore.Mvc.Formatters.Json.Internal;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// An action result which formats the given object as JSON.
    /// </summary>
    public class PartialJsonResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartialJsonResult"/> class.
        /// </summary>
        /// <param name="value">The value to format as JSON.</param>
        public PartialJsonResult(object value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartialJsonResult"/> class.
        /// </summary>
        /// <param name="value">The value to format as JSON.</param>
        /// <param name="serializerSettings">The <see cref="JsonSerializerSettings"/> to be used by
        /// the formatter.</param>
        public PartialJsonResult(object value, JsonSerializerSettings serializerSettings)
        {
            if (serializerSettings == null)
            {
                throw new ArgumentNullException(nameof(serializerSettings));
            }

            this.Value = value;
            this.SerializerSettings = serializerSettings;
        }

        /// <summary>
        /// Gets or sets the <see cref="Net.Http.Headers.MediaTypeHeaderValue"/> representing the Content-Type header of the response.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="JsonSerializerSettings"/>.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the value to be formatted.
        /// </summary>
        public object Value { get; set; }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var services = context.HttpContext.RequestServices;
            var executor = services.GetRequiredService<PartialJsonResultExecutor>();

            return executor.ExecuteAsync(context, this);
        }
    }
}
