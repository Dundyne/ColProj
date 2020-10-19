using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebUwp.Core.Helpers;

namespace WebUwp.Core.DataHandlers
{
    public class CrudHandler
    {
        /// <summary>Logins the specified user.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sc">The sc.</param>
        /// <returns></returns>
        public static async Task<T> Login<T>(StringContent sc)
        {

            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClientPost = new HttpClient();

                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClientPost.PostAsync(
                    UriHelper.AuthenticateUri,
                    sc);

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBodyPost = await httpResponseMessage.Content.ReadAsStringAsync();
                var returnValue = JsonSerializer.Deserialize<T>(httpResponseBodyPost, serializeOptions);
                return returnValue;

            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>Registers the specified user.</summary>
        /// <param name="sc">The sc.</param>
        /// <returns></returns>
        public static async Task<bool> Register(StringContent sc)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClientPost = new HttpClient();

                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClientPost.PostAsync(
                    UriHelper.RegisterUri,
                    sc);

                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBodyPost = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBodyPost);
                return true;
            }
            catch (Exception ex)
            {
                // Write out any exceptions.
                Debug.WriteLine(ex);
                return false;
            }
        }



        /// <summary>Creates the collective.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sc">The sc.</param>
        /// <param name="svcCredentials">The SVC credentials.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task<T> CreateCollective<T>(StringContent sc, string svcCredentials)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClientPost = new HttpClient();
                var headers = httpClientPost.DefaultRequestHeaders;

                headers.Add("Authorization", "Basic " + svcCredentials);

                string header = "ie";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClientPost.PostAsync(
                    UriHelper.CollectivesUri,
                    sc);

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBodyPost = await httpResponseMessage.Content.ReadAsStringAsync();
                var returnValue = JsonSerializer.Deserialize<T>(httpResponseBodyPost, serializeOptions);
                return returnValue;

            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>Creates the post.</summary>
        /// <param name="sc">The sc.</param>
        /// <param name="svcCredentials">The SVC credentials.</param>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task CreatePost(StringContent sc, string svcCredentials)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClientPost = new HttpClient();
                var headers = httpClientPost.DefaultRequestHeaders;

                headers.Add("Authorization", "Basic " + svcCredentials);

                string header = "ie";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClientPost.PostAsync(
                    UriHelper.PostsUri,
                    sc);

                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBodyPost = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBodyPost);
            }
            catch (Exception ex)
            {
                // Write out any exceptions.
                Debug.WriteLine(ex);
            }
        }

        /// <summary>Puts the generic asynchronous.</summary>
        /// <param name="sc">The sc.</param>
        /// <param name="svcCredentials">The SVC credentials.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task<bool> PutGenericAsync(StringContent sc, string svcCredentials, Uri uri)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClientPost = new HttpClient();
                var headers = httpClientPost.DefaultRequestHeaders;

                headers.Add("Authorization", "Basic " + svcCredentials);

                string header = "ie";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }
                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClientPost.PutAsync(
                    uri,
                    sc);

                // Make sure the put succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBodyPost = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBodyPost);
                return true;
            }
            catch (Exception ex)
            {
                // Write out any exceptions.
                Debug.WriteLine(ex);
                return false;
            }
        }

        /// <summary>Deletes the generic asynchronous.</summary>
        /// <param name="svcCredentials">The SVC credentials.</param>
        /// <param name="uri">The URI.</param>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task DeleteGenericAsync(string svcCredentials, Uri uri)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClientPost = new HttpClient();
                var headers = httpClientPost.DefaultRequestHeaders;

                headers.Add("Authorization", "Basic " + svcCredentials);

                string header = "ie";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }
                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClientPost.DeleteAsync(
                    uri
                    );

                // Make sure the put succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBodyPost = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBodyPost);
            }
            catch (Exception ex)
            {
                // Write out any exceptions.
                Debug.WriteLine(ex);
            }
        }



        /// <summary>Joins the collective.</summary>
        /// <param name="sc">The sc.</param>
        /// <param name="svcCredentials">The SVC credentials.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task<bool> JoinCollective(StringContent sc, string svcCredentials)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClientPost = new HttpClient();
                var headers = httpClientPost.DefaultRequestHeaders;

                headers.Add("Authorization", "Basic " + svcCredentials);

                string header = "ie";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClientPost.PostAsync(
                    UriHelper.CollectiveUsersUri,
                    sc);

                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBodyPost = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBodyPost);
                return true;
            }


            catch (Exception ex)
            {
                // Write out any exceptions.
                Debug.WriteLine(ex);
                return false;
            }
        }


        /// <summary>Gets the count of entity.</summary>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task<int> GetCount(Uri requestUri)
        {
            HttpClient httpClient = new HttpClient();
            var headers = httpClient.DefaultRequestHeaders;

            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {

                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                return Convert.ToInt32(httpResponseBody);

            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return 0;
            }
        }
        /// <summary>Gets the generic array asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="svcCredentials">The SVC credentials.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task<IList<T>> GetGenericArrayAsync<T>(Uri requestUri, string svcCredentials)
        {
            HttpClient httpClient = new HttpClient();
            var headers = httpClient.DefaultRequestHeaders;

            headers.Add("Authorization", "Basic " + svcCredentials);

            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {

                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();


                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                var studentArray = JsonSerializer.Deserialize<IList<T>>(httpResponseBody, serializeOptions);

                return studentArray;

            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return null;
            }
        }

        /// <summary>Gets the generic entity asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="svcCredentials">The SVC credentials.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task<T> GetGenericEntityAsync<T>(Uri requestUri, string svcCredentials)
        {
            HttpClient httpClient = new HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            headers.Add("Authorization", "Basic " + svcCredentials);
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {

                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();


                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                var testUser = JsonSerializer.Deserialize<T>(httpResponseBody, serializeOptions);
                return testUser;
            }
            catch (Exception ex)
            {

                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return default;
            }
        }

    }
}
