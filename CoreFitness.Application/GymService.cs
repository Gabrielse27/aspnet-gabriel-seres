using CoreFitness.Application.Common.Results; // Behövs för Result
using CoreFitness.Application.Interfaces;
using CoreFitness.Application.Members.Inputs; // Behövs för JoinMembershipInput
using CoreFitness.Domain.Abstractions;      // Behövs för IMemberRepository
using CoreFitness.Domain.Agregates.Members;
using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Repositoryes.Members;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreFitness.Application.Services
{
    public class GymService : IGymService
    {
        private readonly IMemberRepository _memberRepository;

        // Konstruktor: Här "injicerar" vi repositoryt så vi kan spara i SQL
        public GymService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<GymPass>> GetAllPassesAsync()
        {
            return new List<GymPass>(); // Implementeras senare
        }

        public async Task<bool> BookPassAsync(int passId, string userId)
        {
            return true; // Logik för dubbelbokning läggs till här sen
        }

        // --- MEDLEMSKAP (VG-KRAV) ---

        public async Task<Result> JoinMembershipAsync(JoinMembershipInput input)
        {
            try
            {
                var newMember = Member.Create(input.UserId, input.MembershipId);


                await _memberRepository.AddAsync(newMember);
                await _memberRepository.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure("Kunde inte registrera medlemskapet.");
            }
        }

        public async Task<IEnumerable<Memberships>> GetAllMembershipsAsync()
        {
            // Här anropar du lämpligt repository för att hämta Standard/Premium
            return new List<Memberships>();
        }

        // --- ÖVRIGA METODER (Fylls i vid behov) ---
        public Task<GymPass?> GetPassByIdAsync(int id) => throw new NotImplementedException();
        public Task<IEnumerable<Booking>> GetUserBookingAsync(string userId) => throw new NotImplementedException();
        public Task<bool> CancelBookingAsync(int bookingId, string userId) => throw new NotImplementedException();
    }
}
