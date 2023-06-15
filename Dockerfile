FROM mcr.microsoft.com/dotnet/sdk:6.0 as builder

ADD . /app

WORKDIR /app

RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0

COPY --from=builder /app/publish /app

WORKDIR /app

CMD ["dotnet", "RazorApp.dll", "--urls", "http://0.0.0.0:8080"]