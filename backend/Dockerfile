FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

COPY ./backend/dist .

ENTRYPOINT [ "dotnet", "backend.dll" ]