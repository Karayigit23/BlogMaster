using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.Tag;

 public class UpdateTagCommand : IRequest<Entity.Tag>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateTagHandler : IRequestHandler<UpdateTagCommand,Entity.Tag>
    {
        private readonly ITagRepository _tagRepository;

        public UpdateTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Entity.Tag>  Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetTagById(request.Id);

            if (tag == null)
            {
                //throw new Exception("The tag to be updated was not found.");
            }
            tag.Name = request.Name;

            await _tagRepository.UpdateTag(tag);
            return tag;

            
        }
    }


