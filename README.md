# Shopify.NET

A .NET wrapper around the [Shopify&trade;](http://api.shopify.com/) API.

Initially I intend to make a read-only API..though pull requests are always welcome!

## Running the Sample Application

The app will work with both private and store apps using Shopify's OAuth scheme. 

When you launch the app the first time it will ask you for your app's `Client Id` and 
`Client Secret`. This information will be used to authorize the local MVC app.

Once authorized, the access token will be stored in an environment variable called 
`SHOPIFY_NET_TOKEN`

### Wanna Try it With a Private App?

If you're using a private app, you can just set the environment variable directly using your app's 
`Password` as the access token. This will bypass the standard OAuth flow.

    setx SHOPIFY_NET_TOKEN <your_private_app_password>