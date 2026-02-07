using TemperoDaVovo.Application.UseCases.SideDish.Commands.CreateSideDish;
using TemperoDaVovo.Application.UseCases.SideDishGroup.Queries.GetAllSideDishGroups;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.SideDishGroup.Commands.CreateSideDish;

public class CreateSideDishUseCase : ICreateSideDishUseCase
{
    private readonly ISideDishWriteOnlyRepository _sideDishWriteOnlyRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSideDishUseCase(ISideDishWriteOnlyRepository sideDishWriteOnlyRepository, ISideDishReadOnlyRepository sideDishReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _sideDishWriteOnlyRepository = sideDishWriteOnlyRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<SideDishResponseJson> Execute(CreateSideDishRequestJson request)
    {
        await Validate(request);

        var sideDish = new Domain.Entities.SideDish()
        {
            SideDishGroupId = request.SideDishGroupId,
            Name = request.Name,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            IsActive = true
        };
        
        await _sideDishWriteOnlyRepository.CreateSideDish(sideDish);
        await _unitOfWork.CommitAsync();

        return new SideDishResponseJson()
        {
            Id = sideDish.Id,
            SideDishGroupId = sideDish.SideDishGroupId,
            Name = sideDish.Name,
            Quantity = sideDish.Quantity,
            UnitPrice = sideDish.UnitPrice,
            IsActive = sideDish.IsActive
        };

    }
    
    private async Task Validate(CreateSideDishRequestJson request)
    {
        
        var sideDishGroup = await _sideDishReadOnlyRepository.GetSideDishGroupById(request.SideDishGroupId);
        if (sideDishGroup == null)
            throw new NotFoundException(["Grupo de complementos não encontrados."]);
        
        var validator = new CreateSideDishValidator();
        var result = await validator.ValidateAsync(request);


        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}