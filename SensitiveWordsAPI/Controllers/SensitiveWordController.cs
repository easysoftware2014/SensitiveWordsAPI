using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SensitiveWordsAPI.Helper;
using SensitiveWordsAPI.Model;
using SensitiveWordsAPI.Service.Impl;
using SensitiveWordsAPI.Service.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensitiveWordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensitiveWordController : ControllerBase
    {
        private readonly ISensitiveWords _service;

        public SensitiveWordController(ISensitiveWords sensitiveWords)
        {
            _service = sensitiveWords;
        }
       
        [HttpGet("import-data")]
        private ResponseModel ImportTextData()
        {
            var check = ImportData.ReadFile();

            foreach (var item in check)
            {
                _service.AddWord(item).GetAwaiter().GetResult();
            }

            return new ResponseModel { IsSuccessful = true };
        }
        [HttpPost("add-word")]
        private async Task<object> AddWord(string message)
        {
            var results = await _service.AddWord(message);
            if (results > 0)
                return new ResponseModel { IsSuccessful = true };

            return new ResponseModel { IsSuccessful = false };
        }
        [HttpPost("update-word")]
        private async Task<object> UpdateWord(Guid id, string name)
        {
            var results = await _service.UpdateWord(id, name);
            if (results)
                return new ResponseModel { IsSuccessful = true };

            return new ResponseModel { IsSuccessful = false };
        }
        [HttpPost("remove-word")]
        private async Task<object> RemoveWord(string name)
        {
            var results = await _service.RemoveWord(name);
            if (results)
                return new ResponseModel { IsSuccessful = true };

            return new ResponseModel { IsSuccessful = false };
        }
        [HttpPost("send-message")]
        public async Task<object> GetWord(string message)
        {
            var results = await _service.GetAllWord();
            var words = results.Select(x => x.Name).ToList();
            var splittedMessege = message.Split(" ");
            var output = new StringBuilder();

            foreach (var word in words)
            {
                foreach (var m in splittedMessege)
                {
                    if (word == m)
                    {
                        var star = StarOutSensitiveWord.StarOutWord(m);
                        output.Append(star);
                    }
                    else
                        output.Append(m);
                }
            }

            return new ResponseModel { IsSuccessful = false, Result = output.ToString() };
        }

    }
}
