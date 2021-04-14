using System;
using System.Net;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;
using Manitouage1.Models;
using Manitouage1.Models.ViewModels;

namespace Manitouage1.Controllers
{
    public class ControllersHelper
    {
        public JavaScriptSerializer jss = new JavaScriptSerializer();
        public readonly HttpClient client;
        public readonly string modelName;

        public ControllersHelper( string modelName )
        {
            this.modelName = modelName;

            HttpClientHandler handler = new HttpClientHandler() {
                AllowAutoRedirect = false
            };
            client = new HttpClient( handler );

            // Change this to match your own local port number.
            client.BaseAddress = new Uri( "https://localhost:44397/api/" );

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue( "application/json" ) );

            // This can be used to override certificate rejection issues.
            //ServicePointManager.ServerCertificateValidationCallback += 
            //    ( sender, cert, chain, sslPolicyErrors ) => true;
        }

        /// <summary>
        /// Use the client member to perform a GET request on a given url.
        /// </summary>
        /// <param name="url">A string of the url for the GET request.</param>
        /// <returns>An HttpResponseMessage object containing the result of the request.</returns>
        /// <example>
        /// HttpResponseMessage response = doGetRequest( "ProductsData/GetProductDto/" + productId );
        /// if( response.IsSuccessStatuscode ) { ...
        /// </example>
        public HttpResponseMessage doGetRequest( string url )
        {
            HttpResponseMessage response = client.GetAsync( url ).Result;
            return response;
        }

        /// <summary>
        /// Use the client member to perform a POST request. Use the JSON 
        /// serializer member to convert the obj parameter into JSON format
        /// to standardize the processing by the controller receiving the request.
        /// </summary>
        /// <param name="url">A string of the url for the POST request.</param>
        /// <param name="obj">An object to be put into JSON format to be processed by the receiving controller</param>
        /// <returns>An HttpResponseMessage object containing the result of the request.</returns>
        /// <example>
        /// Product product = new Product { ... };
        /// HttpResponseMessage response = doPostRequest( "ProductsData/UpdateProduct/" + productId, product );
        /// if( response.IsSuccessStatuscode ) { ...
        /// </example>
        public HttpResponseMessage doPostRequest( string url, Object obj )
        {
            HttpContent content = new StringContent( jss.Serialize( obj ) );
            Debug.WriteLine( jss.Serialize( obj ) );
            content.Headers.ContentType = new MediaTypeHeaderValue( "application/json" );
            HttpResponseMessage response = client.PostAsync( url, content ).Result;
            return response;
        }

        /// <summary>
        /// Use the client member to perform a multi-part POST request, like uploading an image. 
        /// </summary>
        /// <param name="url">A string of the url for the POST request.</param>
        /// <param name="requestContent">A MultipartFormDataContent object, likely containing data from an uploaded file.</param>
        /// <returns>An HttpResponseMessage object containing the result of the request.</returns>
        /// <example>
        /// MultipartFormDataContent requestContent = new MultipartFormDataContent();
        /// requestContent.Add( imageContent, "newImage", imageData.FileName );
        /// HttpResponseMessage response = doMultiPartPostRequest( url, requestContent );
        /// if( response.IsSuccessStatusCode ) { ...
        /// </example>
        public HttpResponseMessage doMultiPartPostRequest( string url, MultipartFormDataContent requestContent )
        {
            HttpResponseMessage response = client.PostAsync( url, requestContent ).Result;
            return response;
        }

        /// <summary>
        /// Deserialize the json in the HttpResponseMessage and return the require object.
        /// </summary>
        /// <typeparam name="T">The type of object to construct from the json.</typeparam>
        /// <param name="response">A response from a DataController containing a serialized object of type T.</param>
        /// <returns>If the content exists and can be deserialized into the specified object type, an oject of type T is returned, otherwise a default T.</returns>
        /// <example>
        /// HttpResponseMessage response = doPostRequest( "ProductsData/UpdateProduct/5", product );
        /// if( response.IsSuccessStatusCode ) {
        ///     ProductDto productDto = getFromResponse<ProductDto>( response );
        ///     ...
        /// </example>
        public T getFromResponse<T>( HttpResponseMessage response )
        {
            try {
                string jsonContent = response.Content.ReadAsStringAsync().Result;
                return jss.Deserialize<T>( jsonContent );

            } catch( Exception e ) {
                Debug.WriteLine( e );
                return default(T);
            }
        }

        public T doGetAndGetFromResponse<T>( string url )
        {
            return getFromResponse<T>( doGetRequest( url ) );
        }

        /// <summary>
        /// Since the url strings for the DataControllers follow a pretty basic convention,
        /// use this function to construct the urls consistently.
        /// </summary>
        /// <param name="action">One of Get, Create, Update, or Delete</param>
        /// <param name="id">The id of the object to act upon. An id of 0 will not be added to the url.</param>
        /// <returns>A string with a format of <model name>sData/<action><model name>[/<id>].</returns>
        public string getUrl( string action, int id )
        {
            return getUrl( modelName, action, id );
        }

        /// <summary>
        /// Since the url strings for the DataControllers follow a pretty basic convention,
        /// use this function to construct the urls consistently.
        /// </summary>
        /// <param name="modelName">The name of the model on which to operate.</param>
        /// <param name="action">One of Get, Create, Update, or Delete</param>
        /// <param name="id">The id of the object to act upon. An id of 0 will not be added to the url.</param>
        /// <returns>A string with a format of <model name>sData/<action><model name>[/<id>].</returns>
        public static string getUrl( string modelName, string action, int id )
        {
            string url = modelName + "sData/" + action + modelName;
            if( id != 0 ) {
                url += "/" + id;
            }
            return url;
        }
    }
}