# WorkTrackingProject

WorkTracking This is a solution for collecting statistics on the work performed by employees and maintaining documentation of device repairs.

The solution has a three-tier system. Clint-SignalR Server-SQL Database.
The client receives data in real time. Each change is immediately visible to all running clients.

For a test, run WorkTracking_Server then NewWorkTracking. The server will create a test local database. Next, you need to add the user directly to the database in the dbo.Admins table. The username must exactly match the Asset Directory card

To run the WorkTrackingSite test, you need to add a link to the Install_Printers_Lib_Framework library from the Install Printers project
