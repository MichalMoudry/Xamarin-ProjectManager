﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectManager.Models
{
    class ProjectDatabase
    {
        //Connection   
        private SQLiteAsyncConnection database;
        public ProjectDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Project>().Wait();
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

        public Task<List<T>> GetUnfinishedProjects<T>() where T : new()
        {
            return database.QueryAsync<T>($"SELECT * FROM [Project] WHERE [IsCompleted] = 0");
        }

        public Task<List<T>> GetFinishedProjects<T>() where T : new()
        {
            return database.QueryAsync<T>($"SELECT * FROM [Project] WHERE [IsCompleted] = 1");
        }

    }
}
