using SensitiveWordsAPI.Entities;
using SensitiveWordsAPI.Repository.Interfaces;
using SensitiveWordsAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensitiveWordsAPI.Service.Impl
{
    public class SensitiveWordService : ISensitiveWords
    {
        private readonly ISensitiveWordsRepository _repository;
        public SensitiveWordService(ISensitiveWordsRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> AddWord(string name)
        {
            return await _repository.AddWord(name);
        }

        public async Task<List<Word>> GetAllWord()
        {
            return await _repository.GetAllWord();
        }

        public async Task<string> GetWord(string name)
        {
            return await _repository.GetWord(name);
        }

        public async Task<bool> RemoveWord(string name)
        {
            return await _repository.RemoveWord(name);
        }

        public async Task<bool> UpdateWord(Guid id,string name)
        {
            return await _repository.UpdateWord(id, name);
        }

        Task<List<Word>> ISensitiveWords.GetAllWord()
        {
            throw new NotImplementedException();
        }
    }
}
