

using CoreFitness.Domain.Agregates.Members;
using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Identity;
using CoreFitness.Domain.Repositoryes.Members;
using CoreFitness.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Infrastructure.Repositories.Members;

public class MemberRepository : IMemberRepository
{
    private readonly DataContext _context;

    public MemberRepository(DataContext context)
    {
        _context = context;
        
    }

    
    public async Task AddAsync(Member entity, CancellationToken ct = default)
    {
        var user = await _context.Users.FindAsync(new object[] { entity.UserId }, ct);

        var dbEntity = new MemberEntity
        {
            Id = Guid.NewGuid().ToString(), // Eller hur du genererar ID
            UserId = entity.UserId,
            MembershipId = entity.MembershipId,
            CreatedAt = DateTimeOffset.Now,
            // Om du har namn i entity:
            FirstName = user?.FirstName ?? "Okänt", // Här kan du hämta från inloggningen senare
            LastName = user?.LastName ?? "Okänt" 
        };

        // 2. Lägg till i rätt DbSet (den som är kopplad till SQL)
        await _context.Set<MemberEntity>().AddAsync(dbEntity, ct);

        // 3. Spara ner till SQL
        await _context.SaveChangesAsync(ct);
    }

    // Fix för CS0738: GetAllAsync MÅSTE returnera Task<IReadOnlyList<Member>>
    public async Task<IReadOnlyList<Member>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Set<Member>().ToListAsync(ct);
    }

    
    public async Task<bool> UpdateAsync(Member entity, CancellationToken ct = default)
    {
        _context.Set<Member>().Update(entity);
        var result = await _context.SaveChangesAsync(ct);
        return result > 0;
    }

    // Fix för CS0738: RemoveAsync MÅSTE returnera Task<bool>
    public async Task<bool> RemoveAsync(Member entity, CancellationToken ct = default)
    {
        _context.Set<Member>().Remove(entity);
        var result = await _context.SaveChangesAsync(ct);
        return result > 0;
    }

    public async Task<Member?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return await _context.Set<Member>().FindAsync(new object[] { id }, ct);
    }

    public async Task<Member?> GetMemberByUserIdAsync(string userId, CancellationToken ct = default)
    {
        // Vi hämtar från MemberEntity-tabellen istället
        var entity = await _context.Set<MemberEntity>()
            .FirstOrDefaultAsync(x => x.UserId == userId, ct);

        if (entity == null) return null;

        // Här mappar vi tillbaka från Entity till din domän-Member
        return Member.Create(entity.UserId, entity.MembershipId);
    }

    public Task<string> GetUserId(Member model)
    {
        return Task.FromResult(model.UserId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }   
}