public class RegistrationResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public RegistrationResult()
        {
            Errors = new List<string>();
        }

        public static RegistrationResult Success()
        {
            return new RegistrationResult { Succeeded = true };
        }

        public static RegistrationResult Failure(IEnumerable<string> errors)
        {
            return new RegistrationResult { Succeeded = false, Errors = errors };
        }
    }