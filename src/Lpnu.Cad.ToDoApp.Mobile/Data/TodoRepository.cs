using Lpnu.Cad.ToDoApp.Shared.DataTransferObjects;
using SQLite;

namespace Lpnu.Cad.ToDoApp.Mobile.Data;

internal class TodoRepository
{
    private readonly SQLiteAsyncConnection _db;

    public TodoRepository()
    {
        const string DatabaseFilename = "TodoSQLite.db3";
        var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbpath = Path.Combine(basePath, DatabaseFilename);
        const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        try
        {
            _db = new SQLiteAsyncConnection(dbpath, Flags);
            Task.Run(async () =>
            {
                try
                {
                    await _db.CreateTableAsync<TodoDto>();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        } catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<List<TodoDto>> Get()
    {
        return await _db.Table<TodoDto>().ToListAsync();
    }

    public async Task Add(TodoDto todo)
    {
        await _db.InsertAsync(todo);
    }

    public async Task Update(TodoDto todo)
    {
        await _db.UpdateAsync(todo);
    }

    public async Task Add(IEnumerable<TodoDto> todos)
    {
        await _db.InsertAllAsync(todos);
    }
}
