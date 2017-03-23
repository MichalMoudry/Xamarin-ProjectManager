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

        private SQLiteAsyncConnection database;     
        public ProjectDatabase(string dbPath)      
        {                                          
            database = new SQLiteAsyncConnection(dbPath); 
            database.CreateTableAsync<Project>().Wait(); 
        } 

        public Task<List<Project>> GetItemsAsync()     
        {              
            return database.Table<Project>().ToListAsync();   
        }    

        public Task<Project> GetItemAsync(int id)        
        {                       
            return database.Table<Project>().Where(i => i.ID == id).FirstOrDefaultAsync();  
        }       

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

        public Task<int> DeleteItemAsync(Project item)    
        {                    
            return database.DeleteAsync(item); 
        }

    }
}
