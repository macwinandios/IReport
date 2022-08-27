# IReport
Report writing application with SQL SERVER as backend, using C#, Xamarin, MVVM design pattern and SOLID principles.
Need to continue re-factoring until I successfully implement Constructor Injection for IoC/Dependency Injection so my ViewModels won't have the dependencies they currently have. 
Bugs: When Creating (SQL SERVER), if all fields are not populated by the user, the application CORRECTLY INSERTS to SQL, but the exception "object reference not set to an instance of an object" is displayed even though it INSERTS successfully.
