FROM microsoft/dotnet:2.0.9-runtime
COPY ./src/PetStore.Images/bin/Release/netcoreapp2.0 /app
WORKDIR /app
ENTRYPOINT ["dotnet", "PetStore.Images.dll"]
EXPOSE 5000