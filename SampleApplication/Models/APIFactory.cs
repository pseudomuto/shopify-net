using RestSharp;
using Shopify.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SampleApplication.Models
{
    public class APIFactory
    {
        public static readonly string SHOP_NAME_KEY = "SHOPIFY_NET_STORE";
        public static readonly string ACCESS_TOKEN_KEY = "SHOPIFY_NET_TOKEN";

        public static ShopifyAPI Create()
        {
            return new ShopifyAPI(
                    ConfigurationManager.AppSettings[SHOP_NAME_KEY],
                    GetEnvVar(ACCESS_TOKEN_KEY)
                );
        }

        public static string AuthorizeURL(string apiKey, string redirectURL)
        {
            return string.Format(
                "https://{0}.myshopify.com/admin/oauth/authorize?client_id={1}&scope={2}&redirect_url={3}",
                ConfigurationManager.AppSettings[SHOP_NAME_KEY],
                apiKey,
                "read_themes,read_content,read_products",
                redirectURL
            );
        }

        public static void MakePermanentAuthToken(string apiKey, string secretKey, string tempToken)
        {
            var token = new TempAuthToken(
                    apiKey,
                    secretKey,
                    tempToken
                );

            var accessToken = ShopifyAPI.CreatePermanentAccessToken(
                    ConfigurationManager.AppSettings[SHOP_NAME_KEY], 
                    token
                );

            Environment.SetEnvironmentVariable(
                    ACCESS_TOKEN_KEY, 
                    accessToken, 
                    EnvironmentVariableTarget.User
                );
        }
        
        public static bool RequiresSetup()
        {         
            var token = GetEnvVar(ACCESS_TOKEN_KEY);
            return string.IsNullOrEmpty(token);
        }

        private static string GetEnvVar(string key)
        {
            var target = EnvironmentVariableTarget.User;
            return Environment.GetEnvironmentVariable(key, target);
        }
    }
}