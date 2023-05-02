using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.Tag;


    public class DeleteTagCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTagHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly ITagRepository _tagRepository;

        public DeleteTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
          

            var result =await _tagRepository.GetTagById(request.Id);
            await _tagRepository.DeleteTag(result);
            return Unit.Value;

        }
    }