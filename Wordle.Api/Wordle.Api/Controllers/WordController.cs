using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Data;
using Wordle.Api.Dtos;
using Wordle.Api.Services;

namespace Wordle.Api.Controllers;

/*Routing by Convention. if controller is named SomethingController,
  like we have below with WordController, then the route will be /Something. */
[Route("[controller]")] 
[ApiController]
public class WordController : ControllerBase
{
    private readonly WordService _wordService;

    public WordController(WordService wordService)
    {
        _wordService = wordService;
    }

    [HttpGet]
    public async Task<string> Get()
    {
        return await _wordService.GetRandomWord();
    }

    [HttpGet("GetManyWords")]
    public async Task<IEnumerable<Word>> GetManyWords(int? count)
    {
        return await _wordService.GetSeveralWords(count);
    }

    [HttpPost]
    public async Task<Word> AddWord(string newWord, bool isCommon)
    {
        return await _wordService.AddWord(newWord, isCommon);
    }

    [HttpPost("AddWordFromBody")]
    public async Task<Word> AddWordFromBody([FromBody] WordDto word)
    {
        return await _wordService.AddWord(word.Text, word.IsCommon);
    }
}
