# ForumApp_TestTask

## To start this project on the local computer do the next steps

#### 1. To clone the project use the link: 
https://github.com/NatalitsaPanchenko/ForumApp_TestTask.git

#### 2. Open the project in Visual Studio 2019 -
open solution file: `ForumApp_TestTask.sln`

#### 3. In `Package Manager Console` execute the command:
```
Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
```

#### 4. In the folder `Add_Data` create a new DB with some name:
your_DB_Name.mdf

#### 5. In file `Web.config` find  `AttachDbFilename=|DataDirectory|\*****.mdf`;
```
      <connectionStrings>
        <add name="DefaultConnection" 
             connectionString="Data Source=(LocalDb)\MSSQLLocalDB;
                               AttachDbFilename=|DataDirectory|\*****.mdf;
                               Integrated Security=True" 
             providerName="System.Data.SqlClient" />
      </connectionStrings>
```
  change the name of DB on
`your_DB_Name.mdf`
you will get something like this:
```
    <connectionStrings>
      <add name="DefaultConnection" 
           connectionString="Data Source=(LocalDb)\MSSQLLocalDB;
                             AttachDbFilename=|DataDirectory|\your_DB_Name.mdf;
                             Integrated Security=True" 
           providerName="System.Data.SqlClient" />
    </connectionStrings> 
```

#### 6. Build the project, launch and open your browser  
