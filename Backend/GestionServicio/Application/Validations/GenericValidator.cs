using System.Text.RegularExpressions;

namespace Application.Validations
{
    public class GenericValidator
    {
        // 1. Validate username (8-20 characters, letters, at least one number, no special characters)
        public bool ValidateUsername(string username)
        {
            string pattern = @"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{8,20}$";
            return Regex.IsMatch(username, pattern);
        }

        // 2. Validate password (at least one number, one uppercase letter, 8-30 characters)
        public bool ValidatePassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[a-zA-Z]).{8,30}$";
            return Regex.IsMatch(password, pattern);
        }

        // 3. Validate Identification (10-13 digits, numbers only)
        public bool ValidateIdentification(string id)
        {
            string pattern = @"^\d{10,13}$";
            return Regex.IsMatch(id, pattern);
        }

        // 4. Validate address (20-100 characters)
        public bool ValidateAddress(string address)
        {
            return address.Length >= 20 && address.Length <= 100;
        }

        // 5. Validate address reference (20-100 characters)
        public bool ValidateAddressReference(string addressReference)
        {
            return addressReference.Length >= 20 && addressReference.Length <= 100;
        }

        // 6. Validate phone number (10 digits, numbers only, starts with 09)
        public bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^09\d{8}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        // 7. Validate Turn description (exactly 6 characters: 2 uppercase letters and 4 digits)
        public bool ValidateTurnDescription(string turnDescription)
        {
            string pattern = @"^[A-Z]{2}\d{4}$";
            return Regex.IsMatch(turnDescription, pattern);
        }

        // Additional helper method to validate text with a length range
        public bool ValidateTextLength(string text, int min, int max)
        {
            return text.Length >= min && text.Length <= max;
        }
    }
}
