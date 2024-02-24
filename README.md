# Setting and run project

## Backend .net project

### Add Migrations:

1. cd to CleanArchitecture.Persistence project
2. create migrations folder

   ```
   dotnet ef migrations add \<migration-name\>
   ```

### Run project

1. cd to CleanArchitecture.WebAPI project
2. run project

   ```
   dotnet run
   ```

   Or use run project feature in your IDE

## Admin UI angular project

### Run project

1. Install yarn

   ```
   npm install --global yarn

   ```

2. Install packets

   ```
   yarn install
   ```

3. Run project

   ```
   yarn start
   ```
