using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Agregates.Members;
using CoreFitness.Domain.Agregates.Members;
using System.Xml.Schema;

namespace CoreFitness.Domain.Repositoryes.Members;

public interface IMemberRepository : IRepositoryBase<Member, string>    
{
    Task<Member> GetMemberByUserIdAsync(string userId, CancellationToken ct = default);

    Task<string> GetUserId(Member model);
}
