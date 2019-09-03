namespace ProductionScheduler.Models.UserLogin {
    internal class User {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public AccessLevels AccessLevel { get; set; }
    }
}
