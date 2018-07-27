FROM microsoft/dotnet:2.1-aspnetcore-runtime
COPY ./ /app
WORKDIR /app
ENTRYPOINT ["dotnet", "PetStore.Images.dll"]
EXPOSE 5000