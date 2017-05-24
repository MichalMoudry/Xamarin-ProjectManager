using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectManager.Models
{
    public class TaskDatabase
    {
        //Connection   
        private SQLiteAsyncConnection database;
        public TaskDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ProjTask>().Wait();
        }

        //Get table in List   
        public Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return database.Table<T>().ToListAsync();
        }

        //Get class instance from database 
        public Task<T> GetItemAsync<T>(int id) where T : class, Interfaces.IDatabaseTemplate, new()
        {
            return database.Table<T>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        //Inserting/updating data to database 
        public Task<int> SaveItemAsync<T>(T item) where T : class, Interfaces.IDatabaseTemplate, new()
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        //Deleting 
        public Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            return database.DeleteAsync(item);
        }

        //Searching
        public Task<T> SearchForItem<T>(string query) where T : class, Interfaces.IDatabaseTemplate, new()
        {
            return database.Table<T>().Where(i => i.Name.Contains(query)).FirstOrDefaultAsync();
        }

        //Task database specific method.
        public Task<List<T>> GetCompletedTasksByID<T>(int id) where T : class, Interfaces.IDatabaseTemplate, new()
        {
            return database.QueryAsync<T>($"SELECT * FROM [ProjTask] WHERE [ProjectID] = {id} AND [IsCompleted] = 1 ORDER BY [StartDate] DESC");
        }
        //Task database specific method.
        public Task<List<T>> GetUnfinishedTasksByID<T>(int id) where T : class, Interfaces.IDatabaseTemplate, new()
        {
            return database.QueryAsync<T>($"SELECT * FROM [ProjTask] WHERE [ProjectID] = {id} AND [IsCompleted] = 0 ORDER BY [StartDate] DESC");
        }
        //Task database specific method.
        public Task<List<T>> GetProjectsTasks<T>(int projectId) where T : class, Interfaces.IDatabaseTemplate, new()
        {
            return database.QueryAsync<T>($"SELECT * FROM [ProjTask] WHERE [ProjectID] = {projectId} ORDER BY [StartDate] DESC");
        }
    }
}
