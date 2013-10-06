using Shopify.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApplication.Models
{
    public class APIFactory
    {
        public static readonly string STORE_KEY_NAME = "SHOPIFY_NET_STORE";
        public static readonly string APIKEY_KEY_NAME = "SHOPIFY_NET_APIKEY";
        public static readonly string PASSWORD_KEY_NAME = "SHOPIFY_NET_PASSWORD";

        public static ShopifyAPI Create()
        {
            return new ShopifyAPI(
                    GetEnvVar(STORE_KEY_NAME),
                    GetEnvVar(APIKEY_KEY_NAME),
                    GetEnvVar(PASSWORD_KEY_NAME)
                );
        }

        public static bool RequiresSetup()
        {
            var store = GetEnvVar(STORE_KEY_NAME);
            var apiKey = GetEnvVar(APIKEY_KEY_NAME);
            var password = GetEnvVar(PASSWORD_KEY_NAME);

            return string.IsNullOrEmpty(STORE_KEY_NAME) ||
                string.IsNullOrEmpty(apiKey) || 
                string.IsNullOrEmpty(password);
        }

        private static string GetEnvVar(string key)
        {
            var target = EnvironmentVariableTarget.User;
            return Environment.GetEnvironmentVariable(key, target);
        }
    }
}