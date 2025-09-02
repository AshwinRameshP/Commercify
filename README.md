# Commercify
For building E Commerce Platform


Install dotnet 9 

###used for  ef migration
dotnet tool install --global dotnet-ef
cmd > 
dotnet ef migrations add AddCategory --project ..\Commercify.Infrastructure\Commercify.Infrastructure.csproj

##To docker compose locally
### Scalar (Works Perfectly well!)
localhost:xxxx/scalar 
- Try endpoint works well
###Swagger(NOT working currrently- TO be fixed)
localhost:xxxx/Swagger

### Postgres admin 
Open from docker Url and login with creds from docker-compose.yml
admin@admin.com 
admin

Create server  based on Create Category Table