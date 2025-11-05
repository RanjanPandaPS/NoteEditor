using NoteEditor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteEditor.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllAsync();
        Task<Note> GetByIdAsync(int id);
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
