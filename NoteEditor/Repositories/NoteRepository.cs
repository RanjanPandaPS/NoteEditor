using Microsoft.EntityFrameworkCore;
using NoteEditor.Data;
using NoteEditor.Models;
using NoteEditor.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteEditor.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;
        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            return await _context.Notes
                .OrderByDescending(n => n.UpdatedAt ?? n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Note> GetByIdAsync(int id) => await _context.Notes.FindAsync(id);

        public async Task AddAsync(Note note)
        {
            _context.Notes.Add(note);
            await SaveAsync();
        }

        public async Task UpdateAsync(Note note)
        {
            note.UpdatedAt = DateTime.Now;
            _context.Notes.Update(note);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var note = await GetByIdAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await SaveAsync();
            }
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
