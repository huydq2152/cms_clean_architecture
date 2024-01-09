namespace CleanArchitectureDemo.Domain.Common.Interfaces;

public interface IFullAudited: ICreationAudited, IModificationAudited, IDeletionAudited
{
    
}