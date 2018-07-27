FROM microsoft/aspnetcore:2.0.9
COPY ./src/PetStore.Images/bin/Release/netcoreapp2.0 /app
WORKDIR /app
ENTRYPOINT ["dotnet", "PetStore.Images.dll"]
EXPOSE 5000