namespace BillingService.Tools {
    public class CustomError {
        public string errorHeader { get; set; }
        public string errorBody { get; set; }
        public string errorCode { get; set; }

        public CustomError (string _errorHeader, string _errorBody, string _errorCode) {
            errorHeader = _errorHeader;
            errorBody = _errorBody;
            errorCode = _errorCode;
        }
    }
}
