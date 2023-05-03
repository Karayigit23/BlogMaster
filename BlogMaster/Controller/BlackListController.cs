using BlogMaster.Core.Command.BlackList;
using BlogMaster.Core.Entity;
using BlogMaster.Core.Query.BlackList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogMaster.Controller;
[Route("api/[controller]")]
[ApiController]

public class BlackListController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlackListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<BlackList>> GetAllBlackList()
    {
        var black = await _mediator.Send(new GetAllBlackListQuery());
        return black;
    }

    [HttpGet("{id}")]
    public async Task<BlackList> GetBlackListById(int id)
    {
        var black = await _mediator.Send(request: new GetBlackListByIdQuery { Id = id });
        return black;
    }

    [HttpGet("article/{articleId}")]
    public async Task<List<BlackList>> GetBlackListByArticleId(int articleId)
    {
        var black = await _mediator.Send(request: new GetBlackListByArticleIdQuery { ArticleId = articleId });
        return black;
    }

    [HttpGet("user/{userId}")]
    public async Task<List<BlackList>> GetBlackListByUserId(int userId)
    {
        var black = await _mediator.Send(request: new GetBlackListByUserIdQuery { UserId = userId });
        return black;
    }

    [HttpPost]
    public async Task AddBlackList([FromBody] BlackList blackList)
    {
        await _mediator.Send(blackList);
    }

    [HttpDelete("{id}")]
    public async Task DeleteBlackList(int id)
    {
        await _mediator.Send(new DeleteBlackListCommand { Id = id });
    }

}