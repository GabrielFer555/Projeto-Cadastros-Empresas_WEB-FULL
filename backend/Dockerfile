FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build-env

WORKDIR /app

#restaura dependencias
COPY *.csproj ./
RUN dotnet restore

#copia arquivos de projeto e faz o release
COPY . ./
RUN dotnet publish -c release -o out


FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "backend.dll" ]

