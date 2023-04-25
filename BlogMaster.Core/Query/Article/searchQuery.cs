namespace BlogMaster.Core.Query.Article;

public class searchQuery
{
    public class ArticleRepository
    {
        private List<Entity.Article> _articles; // Makalelerinizi temsil eden bir koleksiyon

        public ArticleRepository(List<Entity.Article> articles)
        {
            _articles = articles;
        }

        public async Task<List<Entity.Article>> SearchArticles(string category = null, string tagId = null, string author = null)
        {
            // Kullanıcının girdiği kategori, etiket ve yazar parametrelerine göre makaleleri filtreleme işlemleri
            var filteredArticles = _articles;

            if (!string.IsNullOrEmpty(category))
            {
                filteredArticles = filteredArticles.Where(a => a.Category.Name == category).ToList();
            }

            if (!string.IsNullOrEmpty(tagId))
            {
                filteredArticles = filteredArticles.Where(a => a.Tag.Name==tagId).ToList();
            }

            if (!string.IsNullOrEmpty(author))
            {
                filteredArticles = filteredArticles.Where(a => a.AuthorUser.UserName == author).ToList();
            }

            return filteredArticles;
        }
    }
}