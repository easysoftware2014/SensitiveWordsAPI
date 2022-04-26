using Microsoft.AspNetCore.Mvc;
using SensitiveWordsAPI.Helper;
using SensitiveWordsAPI.Model;
using SensitiveWordsAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
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
       /// <summary>
       /// Use this endpoint to import data (sensitive words)
       /// change access modifier to public
       /// </summary>
       /// <returns></returns>
        [HttpGet("import-data")]        
        private ResponseModel ImportTextData()
        {
            var check = ImportData.ReadFile();
            
            foreach (var item in check)
            {
                var newItem = RemoveSpecialCharacters(item);

                if(!string.IsNullOrEmpty(newItem))
                    _service.AddWord(newItem).GetAwaiter().GetResult();
            }

            return new ResponseModel { IsSuccessful = true, Result = "Data uploaded successfully" };
        }
        private string RemoveSpecialCharacters(string str)
        {
            var sb = new StringBuilder();

            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
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
        /// <summary>
        /// Use this endpoint to filter sensitive message
        /// bloop out the words
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("send-message")]
        public async Task<object> GetWord(string message)
        {
            var results = await _service.GetAllTheWords();
            var words = results.Select(x => x.Name).ToList();
            var splittedMessege = message.Split(" ");
            var output = new StringBuilder();
            var temp = new List<string>();

            foreach (var m in splittedMessege)
            {
                foreach (var word in words)
                {
                    if (word.ToUpper() == m.ToUpper())
                    {
                        var star = StarOutSensitiveWord.StarOutWord(m);
                        output.Append(star+ " ");
                        temp.Add(m);
                    }
                }
                if (!temp.Contains(m))
                    output.Append(m + " ");
            }           

            return new ResponseModel { IsSuccessful = true, Result = output.ToString() };
        }

    }
}
