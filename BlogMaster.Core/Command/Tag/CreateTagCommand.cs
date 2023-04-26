using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using MediatR;


namespace BlogMaster.Core.Command.Tag
{
    public class CreateTagCommand : IRequest<Entity.Tag>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateTagHandler : IRequestHandler<CreateTagCommand, Entity.Tag>
    {
        private readonly ITagRepository _tagRepository;

        public CreateTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Entity.Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = new Entity.Tag
            {
                Id = request.Id,
                Name = request.Name,
                Articles = new List<Article>() // Eğer Articles listesi de eklenmek isteniyorsa burada initialize edilebilir
            };

            await _tagRepository.AddTag(tag);
            return tag;
        }
    }
}
