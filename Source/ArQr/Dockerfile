# Publish project before trying to dockerize.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY bin/Release/net5.0/publish ./

# Two next commands are due to an error realted to "Invariant globalization"
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ["dotnet", "ArQr.dll"]
