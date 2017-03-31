using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectManager.Models
{
    public class ProjectDatabase
    {
        //Connection
        private SQLiteAsyncConnection database;     
        public ProjectDatabase(string dbPath)      
        {                                          
            database = new SQLiteAsyncConnection(dbPath); 
            database.CreateTableAsync<Project>().Wait(); 
        }

        //Get table in List.
        public Task<List<T>> GetItemsAsync<T>() where T : new()     
        {              
            return database.Table<T>().ToListAsync();   
        }    

        //Get class instance from database.
        public Task<Project> GetItemAsync(int id)        
        {                       
            return database.Table<Project>().Where(i => i.ID == id).FirstOrDefaultAsync();  
        }       

        //Inserting/Updating data to database.
        public Task<int> SaveItemAsync(Project item) 
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

        //Deleting.
        public Task<int> DeleteItemAsync<T>(T item)    
        {                    
            return database.DeleteAsync(item); 
        }

    }
}
