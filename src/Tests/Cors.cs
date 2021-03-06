// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Tests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Xunit;

    public class Cors
    {
        [Fact]
        public void EnsureCors()
        {
            using (HttpClient client = ApiHttpClient.Create($"{Globals.TEST_SERVER}:{Globals.TEST_PORT}")) {

                HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("OPTIONS"), $"{Globals.TEST_SERVER}:{Globals.TEST_PORT}/api");

                message.Headers.Add("Access-Control-Request-Headers", "X-PINGOTHER");
                message.Headers.Add("Access-Control-Request-Method", "GET");

                // Origin is irrelevant, but the header must be present
                message.Headers.Add("Origin", "https://localhost:51772");

                HttpResponseMessage res = client.SendAsync(message).Result;

                Assert.True(res.StatusCode == HttpStatusCode.NoContent);
                Assert.True(res.Headers.Contains("Access-Control-Allow-Origin"));
            }
        }
    }
}
