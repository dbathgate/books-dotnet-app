FROM mcr.microsoft.com/dotnet/sdk:6.0 as builder

ADD . /app

WORKDIR /app

RUN dotnet publish -c Release --self-contained -r linux-x64 -o publish

FROM alpine

COPY --from=builder /app/publish /app

WORKDIR /app

CMD ["./RazorApp", "--urls", "http://0.0.0.0:8080"]