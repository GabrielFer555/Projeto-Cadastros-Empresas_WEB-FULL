FROM microsoft/dotnet:2.2-aspnetcore:runtime

WORKDIR /app

COPY ./backend/dist .

ENTRYPOINT [ "dotnet", "backend.dll" ]