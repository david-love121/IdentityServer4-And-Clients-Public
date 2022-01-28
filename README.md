
# IdentityServer4 And Clients
**This project was selected to win a Congressional App Challenge award in November of 2020**

This project combines an Identityserver4 authority and endpoint for external sign in, for use with ASP.NET.
The csproj files represent single projects, that I then combine into a single .sln for use in visual studio. This project was hosted on an AWS server up until 2021 when I shut it down for operating costs. Unfortunately, I had to effectively commit this entire project as one massive commit, to prevent the sensative info from showing up in the history. On my private repository, this project started on July 22, 2020.
### ExampleClient
This is an example of how active user authentication should look. This is configured for external login with IDSEmpty. This is what the client goes to initially, and is then redirected to external log out at IDSEmpty. This application uses the "code" authentication scope. The majority of setup with the identityserver can be found in Startup.cs.
### New API
This is a restructed form of the previous API. This is a protected web API that uses bearer tokens recieved from the authority and manages all of the database operations. When it recieves a proper bearer token, it will process the request as normal, but it returns an HTTP 401 if it recieves a bearer token that was not issued by the authority.
### IDSAuthority
This is the actual login server. Both ExampleClient and IDS4LoginEndpoint use this for signin. This is setup to take two arguments, returnUrl for sign in and returnUrl for signout. So to run this, you will want to call something like "dotnet run https://example.com/Main https://example.com" going to signout-oidc is unecessary after signout, as all the login cookies are cleared manually. I found this method results in less redirects and traffic.


