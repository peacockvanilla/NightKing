amespace MVC5_full_version.Controllers
{
    public class GoogleAuthenticatorViewModel
    {
        public string SecretKey { get; set; }
        public string BarcodeUrl { get; set; }
        public string Code { get; internal set; }
    }
}