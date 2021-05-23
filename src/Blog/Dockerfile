FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
COPY Blog/. ./
RUN dotnet publish "Blog.csproj" -c Release -o output
FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=build-env /app/output/wwwroot .
COPY Blog/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
