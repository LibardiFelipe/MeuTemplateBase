namespace TemplateBase.Domain.Classes
{
    public class MyGoogleCredential
    {
        /* 
         * THOSE INFORMATIONS CAN BE FOUND IN THE SERVICEACCOUNTKEY.JSON
         * LOCATED IN https://console.firebase.google.com/u/0/project/YOUR_PROJECT_ID/settings/serviceaccounts/adminsdk
        */

        public string Type { get; set; } = "TYPE";
        public string Project_id { get; set; } = "PROJECT_ID";
        public string Private_key_id { get; set; } = "PRIVATE_KEY_ID";
        public string Private_key { get; set; } = "PRIVATE_KEY";
        public string Client_email { get; set; } = "CLIENT_EMAIL";
        public string Client_id { get; set; } = "CLIENT_ID";
        public string Auth_uri { get; set; } = "AUTH_URI";
        public string Token_uri { get; set; } = "TOKEN_URI";
        public string Auth_provider_x509_cert_url { get; set; } = "AUTH_PROVIDER_X509_CERT_URL";
        public string Client_x509_cert_url { get; set; } = "CLIENT_X509_CERT_URL";
    }
}
