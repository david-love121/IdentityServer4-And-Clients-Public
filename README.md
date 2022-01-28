# This project was selected to win a Congressional App Challenge award in November of 2020
# IdentityServer4 And Clients
This project combines an Identityserver4 authority and endpoint for external sign in, for use with ASP.NET.
The csproj files represent single projects, that I then combine into a single .sln for use in visual studio (not included in the repos.) To make this, either open a csproj file in visual studio or type dotnet new sln in the working directory. Unfortunately, I had to effectively commit this entire project as one massive commit, to prevent the sensative info from showing up in the history. On my private repository, this project started on July 22, 2020 and won a congressional app award in November of 2020.
### ExampleClient
This is an example of how active user authentication should look. This is configured for external login with IDSEmpty. This is what the client goes to initially, and is then redirected to external log out at IDSEmpty. This application uses the "code" authentication scope. The majority of setup with the identityserver can be found in Startup.cs.
### IDS4LoginEndpoint
This was the simpliest one to create, as M2M communications require far less work to make secure. This is a web API configured to require authorization (not authentication, roles have not been implemented in this project), so one can serve sensative info from a web API without needing to worry about that information being exposed. If the person requesting info is not authorized, they will get a 401 forbidden. This project needs to be configured to serve the info that the client wants it to, which can be done in LoginEndpoint.cs
### IDSEmpty
This is the actual login server. Both ExampleClient and IDS4LoginEndpoint use this for signin. This is setup to take two arguments, returnUrl for sign in and returnUrl for signout. So to run this, you will want to call something like "dotnet run https://example.com/Main https://example.com" going to signout-oidc is unecessary after signout, as all the login cookies are cleared manually. I found this method results in less redirects and traffic.


