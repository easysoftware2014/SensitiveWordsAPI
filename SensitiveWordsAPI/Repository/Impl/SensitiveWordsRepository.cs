using Microsoft.EntityFrameworkCore;
using SensitiveWordsAPI.Entities;
using SensitiveWordsAPI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensitiveWordsAPI.Repository.Impl
{
    public class SensitiveWordsRepository : ISensitiveWordsRepository
    {
        private readonly SensitiveWordDbContext _dbContext;

        public SensitiveWordsRepository(SensitiveWordDbContext context)
        {
            _dbContext = context;
        }
        public async Task<int> AddWord(string name)
        {
            try
            {
                _dbContext.Words.Add(new Word { Name = name });

                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                return 0;                
            }
        }

        public async Task<List<Word>> GetAllWord()
        {
            try
            {
                return await _dbContext.Words.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GetWord(string name)
        {
            try
            {
                var entity = await _dbContext.Words.FindAsync(name);

                if(entity != null && !string.IsNullOrEmpty(entity.Name))
                    return entity.Name;

                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                return string.Empty;

            }
        }

        public async Task<bool> RemoveWord(string name)
        {
            try
            {
                var word = await _dbContext.Words.FindAsync(name);
                var results = _dbContext.Remove(word);

                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                return false;

            }
        }

        public async Task<bool> UpdateWord(Guid id, string name)
        {
            try
            {
                var word = await _dbContext.Words.FindAsync(id);
                word.Name = name;

                return _dbContext.Update(word) == null;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                return false;

            }
        }
    }
}
