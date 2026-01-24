using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Application.Constants.Messages
{
    public static class Signup
    {
        public const string EmailExists = "A user already exists with this email!";
        public const string PhoneExists = "A user already exists with this phone number!";
        public const string RegistrationSuccess = "User registered successfully.";
        public const string ClinicPhoneExists = "This clinic phone number already exists!";
        public const string ClinicNameExists = "This clinic name already exists!";
        public const string ClinicEmailExists = "This clinic email already exists!";
        public const string DrugLicenseExists = "A user already exists with this drug license number!";
        public const string GstNumberExists = "A user already exists with this GST number!";
        public const string DrugLicenseRequired = "Drug license number is required.";
        public const string InvalidDrugLicense = "Please enter a valid drug license number.";
        public const string InvalidGstNumber = "Please enter a valid GST number.";
        public const string ClinicAddressExists = "This clinic address already exists!";
    }
}
