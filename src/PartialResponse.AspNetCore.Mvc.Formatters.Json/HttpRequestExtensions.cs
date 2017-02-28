﻿// Copyright (c) Arjen Post. See License.txt and Notice.txt in the project root for license information.

using System;
using PartialResponse.Core;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpRequestExtensions
    {
        private const string BypassPartialResponse = "BypassPartialResponse";

        /// <summary>
        /// Sets a value indicating whether partial response should be bypassed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="request">The HTTP request.</param>
        public static void SetBypassPartialResponse(this HttpRequest request, bool value)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            request.HttpContext.Items.Add(BypassPartialResponse, value);
        }

        /// <summary>
        /// Gets a value indicating whether partial response should be bypassed.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>True if partial response should be bypassed, otherwise false.</returns>
        public static bool GetBypassPartialResponse(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            object value;

            if (request.HttpContext.Items.TryGetValue(BypassPartialResponse, out value))
            {
                return (bool)value;
            }

            return false;
        }

        internal static bool TryGetFields(this HttpRequest request, out Fields? result)
        {
            if (!request.Query.ContainsKey("fields"))
            {
                result = null;

                return true;
            }

            Fields fields;

            if (!Fields.TryParse(request.Query["fields"][0], out fields))
            {
                result = null;

                return false;
            }

            result = fields;

            return true;
        }
    }
}