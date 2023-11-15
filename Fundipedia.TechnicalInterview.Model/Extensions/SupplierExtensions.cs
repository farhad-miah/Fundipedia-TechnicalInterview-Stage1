using Fundipedia.TechnicalInterview.Model.Supplier;
using System.Text.RegularExpressions;

namespace Fundipedia.TechnicalInterview.Model.Extensions;

public static class SupplierExtensions
{
    public static bool IsActive(this Supplier.Supplier supplier)
    {
        return supplier?.ActivationDate != null;
    }

    public static bool IsValidEmail(this Supplier.Supplier supplier, out string invalidEmail)
    {
        invalidEmail = null;

        if (supplier.Emails is null)
        {
            return false;
        }

        //email regex
        string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

        foreach (Email email in supplier.Emails)
        {
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(email.EmailAddress))
            {
                invalidEmail = email.EmailAddress;

                return false;
            }
        }

        return true;
    }

    public static bool IsValidPhoneNumber(this Supplier.Supplier supplier, out string invalidPhoneNumber)
    {
        invalidPhoneNumber = null;

        if (supplier.Phones is null)
        {
            return false;
        }

        foreach (Phone phone in supplier.Phones)
        {
            if (!(int.TryParse(phone.PhoneNumber, out var number) && phone.PhoneNumber.Length <= 10 && number >= 0))
            {
                invalidPhoneNumber = phone.PhoneNumber;
                return false;
            }
        }

        return true;
    }
}