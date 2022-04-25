using SensitiveWordsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensitiveWordsAPI.Repository.Interfaces
{
    public interface ISensitiveWordsRepository
    {
        Task<int> AddWord(string name);
        Task<bool> RemoveWord(string name);
        Task<bool> UpdateWord(Guid id, string name);
        Task<string> GetWord(string name);
        Task<List<Word>> GetAllWord();
    }
}
