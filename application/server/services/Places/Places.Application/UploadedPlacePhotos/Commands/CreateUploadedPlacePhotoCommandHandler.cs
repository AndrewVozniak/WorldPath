using AutoMapper;
using MediatR;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Application.UploadedPlacePhotos.Commands;

public class CreateUploadedPlacePhotoCommandHandler : IRequestHandler<CreateUploadedPlacePhotoCommand, UploadedPlacePhotoViewModel>
{
    private readonly IMongoDb _mongoDb;
    private readonly IMapper _mapper;

    public CreateUploadedPlacePhotoCommandHandler(IMongoDb mongoDb, IMapper mapper)
    {
        _mongoDb = mongoDb;
        _mapper = mapper;
    }
    
    public async Task<UploadedPlacePhotoViewModel> Handle(CreateUploadedPlacePhotoCommand request, CancellationToken cancellationToken)
    {
        var uploadedPhoto = _mapper.Map<UploadedPlacePhoto>(request);
        await _mongoDb.AddOneUploadedPlacePhoto(uploadedPhoto);
        return _mapper.Map<UploadedPlacePhotoViewModel>(uploadedPhoto);
    }
}