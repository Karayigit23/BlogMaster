namespace BlogMaster.MiddleWares;

public class PaginationMiddleware
{
    private readonly RequestDelegate _next;

    public PaginationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        //gönderilen requeste page varmı diye bakıyor varsa bunu alıup page koyuyo yoksa 1 yapıyor
        var page = context.Request.Query.ContainsKey("page") ? Convert.ToInt32(context.Request.Query["page"]) : 1;
        
        var pageSize = context.Request.Query.ContainsKey("pageSize") ? Convert.ToInt32(context.Request.Query["pageSize"]) : 10;
        
        var skip = (page - 1) * pageSize;

        context.Request.Headers.Add("X-Pagination-Page", page.ToString());
        context.Request.Headers.Add("X-Pagination-PageSize", pageSize.ToString());
        context.Request.Headers.Add("X-Pagination-Skip", skip.ToString());

       
        await _next(context);
    }
}