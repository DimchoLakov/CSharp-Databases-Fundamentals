using System.Linq;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Bonus
    {
        private const string VetNotFound = "Vet with phone number {0} not found!";
        private const string SuccessfullyChangedProfession = "{0}'s profession updated from {1} to {2}.";

        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            var vet = context
                .Vets
                .FirstOrDefault(v => v.PhoneNumber == phoneNumber);

            if (vet == null)
            {
                return string.Format(VetNotFound, phoneNumber);
            }

            var vetOldProfession = vet.Profession;

            vet.Profession = newProfession;

            return string.Format(SuccessfullyChangedProfession, vet.Name, vetOldProfession, newProfession);
        }
    }
}
